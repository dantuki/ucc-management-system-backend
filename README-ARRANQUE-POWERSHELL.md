# Arranque desde cero — PowerShell

Estado validado: Windows 11, PowerShell, SDK .NET 8.0.424 y 10.0.302. Docker Compose está instalado, pero el daemon no estaba disponible durante la auditoría; inicie Docker Desktop antes de ejecutar los pasos de MySQL.

## Paso 0: preparación inicial

```powershell
Set-Location C:\Users\usuario\Downloads\ucc-management-system-backend-main\siga-ucc-backend-modernization
$env:MYSQL_PASSWORD = Read-Host 'MYSQL_PASSWORD' -AsSecureString | ConvertFrom-SecureString -AsPlainText
$env:MYSQL_ROOT_PASSWORD = Read-Host 'MYSQL_ROOT_PASSWORD' -AsSecureString | ConvertFrom-SecureString -AsPlainText
$env:MYSQL_PORT = '3307'
```

El puerto local es `127.0.0.1:3307`, no `3306`. No se imprime ninguna contraseña.

## Terminal 1 — base de datos

```powershell
Set-Location C:\Users\usuario\Downloads\ucc-management-system-backend-main\siga-ucc-backend-modernization
.\scripts\dev\Start-Database.ps1
docker compose ps mysql
```

`docker compose ps` debe mostrar el servicio; si Docker informa que no puede conectarse al daemon, inicie Docker Desktop y repita. Para detener sin eliminar datos: `./scripts/dev/Stop-Development.ps1`.

## Terminal 2 — migraciones locales

Las migraciones solo se habilitan contra la base demo `siga_ucc_demo` y nunca contra una base institucional.

```powershell
Set-Location C:\Users\usuario\Downloads\ucc-management-system-backend-main\siga-ucc-backend-modernization
dotnet tool install --global dotnet-ef
$env:ConnectionStrings__DefaultConnection = '<conexión local a siga_ucc_demo>'
$env:SIGA_UCC_ALLOW_LOCAL_MIGRATIONS = 'true'
.\scripts\dev\Initialize-LocalDatabase.ps1
```

No se verificó esta secuencia porque el daemon de Docker no estaba disponible. No ejecute `database update` contra otro nombre de base.

## Terminal 3 — API

```powershell
Set-Location C:\Users\usuario\Downloads\ucc-management-system-backend-main\siga-ucc-backend-modernization\WebApi
dotnet user-secrets init
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' '<conexión local>'
dotnet user-secrets set 'Jwt:SigningKey' '<secreto aleatorio de al menos 64 bytes>'
Set-Location ..
$env:ConnectionStrings__DefaultConnection = '<conexión local>'
$env:Jwt__SigningKey = '<secreto de sesión>'
.\scripts\dev\Start-Api.ps1
```

Health: `https://localhost:7080/health`; readiness: `https://localhost:7080/health/ready`; Swagger se muestra en Development. Detenga con `Ctrl+C`.

## Terminal 4 — pruebas

```powershell
Set-Location C:\Users\usuario\Downloads\ucc-management-system-backend-main\siga-ucc-backend-modernization
dotnet restore WebApi\WebApi.sln
dotnet build WebApi\WebApi.sln -c Release --no-restore
dotnet test WebApi\WebApi.sln -c Release --no-build
.\scripts\tests\Run-AllTests.ps1
```

El build y las dos pruebas unitarias sí fueron ejecutados. El último script falla deliberadamente si NuGet informa vulnerabilidades altas o críticas; las pruebas de integración, seguridad, arquitectura y rendimiento están **NOT RUN/BLOCKED**.

## Terminal 5 — demostración

La demostración E2E no debe inventarse: requiere MySQL efímero funcional, migraciones comprobadas y bootstrap administrativo aprobado. Consulte `README-DEMO-RECLUTADORES.md` y `README-PRUEBAS-Y-CASOS-DE-USO.md` para el estado real.
