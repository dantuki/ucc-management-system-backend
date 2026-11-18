[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$DatabasePassword,

    [string]$DatabaseName = "siga_ucc_migration_test",
    [string]$DatabaseUser = "siga_migration",
    [int]$Port = 3309
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
    throw "Docker CLI no está disponible en PATH."
}

$repoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
$composeFile = Join-Path $repoRoot "compose.migration-test.yaml"
$container = "siga-ucc-migration-verify-mysql"
$previousConnection = $env:ConnectionStrings__DefaultConnection
$previousPassword = $env:MIGRATION_TEST_MYSQL_PASSWORD
$previousRootPassword = $env:MIGRATION_TEST_MYSQL_ROOT_PASSWORD
$previousDatabase = $env:MIGRATION_TEST_DATABASE
$previousUser = $env:MIGRATION_TEST_USER
$previousPort = $env:MIGRATION_TEST_MYSQL_PORT

function Invoke-MySql {
    param(
        [Parameter(Mandatory)] [string]$Sql,
        [switch]$ExpectFailure
    )

    & docker exec -e "MYSQL_PWD=$DatabasePassword" $container mysql -u $DatabaseUser -D $DatabaseName --batch --skip-column-names -e $Sql
    $exitCode = $LASTEXITCODE

    if ($ExpectFailure) {
        if ($exitCode -eq 0) { throw "Se esperaba un error de integridad para la sentencia SQL." }
        Write-Host "PASS expected integrity rejection"
        return
    }

    if ($exitCode -ne 0) { throw "mysql terminó con código $exitCode." }
}

function Assert-Contains {
    param([string]$Actual, [string]$Expected, [string]$Label)
    if ($Actual -notmatch [regex]::Escape($Expected)) {
        throw "${Label}: no se encontró '$Expected'."
    }
    Write-Host "PASS ${Label}: $Expected"
}

try {
    if (Test-Path $composeFile -PathType Leaf) { } else { throw "No se encontró $composeFile." }
    if (docker container inspect $container *> $null) {
        throw "El contenedor temporal $container ya existe. No se reutiliza para evitar pruebas contra datos desconocidos."
    }

    $env:MIGRATION_TEST_MYSQL_PASSWORD = $DatabasePassword
    $env:MIGRATION_TEST_MYSQL_ROOT_PASSWORD = $DatabasePassword
    $env:MIGRATION_TEST_DATABASE = $DatabaseName
    $env:MIGRATION_TEST_USER = $DatabaseUser
    $env:MIGRATION_TEST_MYSQL_PORT = $Port

    & docker compose -f $composeFile --project-name siga-ucc-migration-verify up -d
    if ($LASTEXITCODE -ne 0) { throw "No se pudo iniciar el MySQL temporal." }

    $deadline = (Get-Date).AddMinutes(3)
    do {
        Start-Sleep -Seconds 3
        $health = docker inspect --format '{{.State.Health.Status}}' $container
    } while ($health -ne "healthy" -and (Get-Date) -lt $deadline)
    if ($health -ne "healthy") { throw "El MySQL temporal no alcanzó estado healthy." }
    Write-Host "PASS isolated MySQL healthy on port $Port"

    $env:ConnectionStrings__DefaultConnection = "Server=127.0.0.1;Port=$Port;Database=$DatabaseName;User Id=$DatabaseUser;Password=$DatabasePassword;"
    Push-Location $repoRoot
    try {
        dotnet ef database update --project Persistence\Persistence.csproj --startup-project WebApi\WebApi.csproj --configuration Release --no-build
        if ($LASTEXITCODE -ne 0) { throw "La primera ejecución de migraciones falló." }
        Write-Host "PASS migrations first execution"

        dotnet ef database update --project Persistence\Persistence.csproj --startup-project WebApi\WebApi.csproj --configuration Release --no-build
        if ($LASTEXITCODE -ne 0) { throw "La segunda ejecución de migraciones falló." }
        Write-Host "PASS migrations idempotent second execution"
    }
    finally {
        Pop-Location
    }

    $tables = Invoke-MySql "SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE() ORDER BY table_name;" | Out-String
    foreach ($table in @("admin.Rol", "EstadoCivil", "Genero", "TipoFormacion", "TipoIdentificacion", "Usuario", "Persona", "Estudiante")) {
        Assert-Contains -Actual $tables -Expected $table -Label "table created"
    }

    Invoke-MySql "INSERT INTO Genero (Nombre, Habilitado) VALUES ('Prueba', 1); INSERT INTO EstadoCivil (Nombre, Habilitado) VALUES ('Prueba', 1); INSERT INTO TipoIdentificacion (Nombre, Abreviatura, Habilitado) VALUES ('Prueba', 'PRB', 1); INSERT INTO Persona (nombres, apellidos, documento, correo, telefono, id_genero, id_estado_civil) VALUES ('Ada', 'Prueba', 'P-100', 'ada@example.test', '3000000000', 1, 1); INSERT INTO Usuario (nombre_usuario, contrasena, habilitado, fecha_creacion, id_persona) VALUES ('ada.test', 'not-a-production-secret', 1, UTC_TIMESTAMP(), 1); INSERT INTO Estudiante (codigo, nombres, apellidos, documento, fecha_nacimiento, correo, id_tipo_identificacion, id_genero, id_estado_civil, id_usuario) VALUES ('EST-100', 'Ada', 'Prueba', 'E-100', '2000-01-01', 'ada.student@example.test', 1, 1, 1, 1);"
    Write-Host "PASS Persona -> Usuario -> Estudiante insert"

    $relationship = Invoke-MySql "SELECT CONCAT(e.codigo, '|', u.nombre_usuario, '|', p.documento) FROM Estudiante e INNER JOIN Usuario u ON u.id_usuario = e.id_usuario INNER JOIN Persona p ON p.id_persona = u.id_persona;" | Out-String
    Assert-Contains -Actual $relationship -Expected "EST-100|ada.test|P-100" -Label "relationship query"

    $foreignKeys = Invoke-MySql "SELECT constraint_name FROM information_schema.referential_constraints WHERE constraint_schema = DATABASE() AND table_name = 'Estudiante' ORDER BY constraint_name;" | Out-String
    foreach ($foreignKey in @("FK_Estudiante_EstadoCivil_id_estado_civil", "FK_Estudiante_Genero_id_genero", "FK_Estudiante_TipoIdentificacion_id_tipo_identificacion", "FK_Estudiante_Usuario_id_usuario")) {
        Assert-Contains -Actual $foreignKeys -Expected $foreignKey -Label "foreign key"
    }

    $indexes = Invoke-MySql "SELECT index_name FROM information_schema.statistics WHERE table_schema = DATABASE() AND table_name = 'Estudiante' AND non_unique = 0 ORDER BY index_name;" | Out-String
    foreach ($index in @("IX_Estudiante_codigo", "IX_Estudiante_documento", "IX_Estudiante_id_usuario")) {
        Assert-Contains -Actual $indexes -Expected $index -Label "unique index"
    }

    Invoke-MySql -ExpectFailure "INSERT INTO Estudiante (codigo, nombres, apellidos, documento, fecha_nacimiento, correo, id_tipo_identificacion, id_genero, id_estado_civil, id_usuario) VALUES ('EST-100', 'Ada', 'Duplicada', 'E-200', '2000-01-01', 'duplicate@example.test', 1, 1, 1, 1);"
    Invoke-MySql -ExpectFailure "INSERT INTO Usuario (nombre_usuario, contrasena, habilitado, fecha_creacion, id_persona) VALUES ('ada.duplicate', 'not-a-production-secret', 1, UTC_TIMESTAMP(), 1);"
    Invoke-MySql -ExpectFailure "INSERT INTO Estudiante (codigo, nombres, apellidos, documento, fecha_nacimiento, correo, id_tipo_identificacion, id_genero, id_estado_civil, id_usuario) VALUES ('EST-200', 'Inválido', 'FK', 'E-300', '2000-01-01', 'invalid@example.test', 1, 1, 1, 999);"

    Write-Host "PASS migration recovery integration suite"
}
finally {
    $env:ConnectionStrings__DefaultConnection = $previousConnection
    $env:MIGRATION_TEST_MYSQL_PASSWORD = $previousPassword
    $env:MIGRATION_TEST_MYSQL_ROOT_PASSWORD = $previousRootPassword
    $env:MIGRATION_TEST_DATABASE = $previousDatabase
    $env:MIGRATION_TEST_USER = $previousUser
    $env:MIGRATION_TEST_MYSQL_PORT = $previousPort
}
