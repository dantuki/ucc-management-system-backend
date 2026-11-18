# UCC Management System Backend

**API REST de gestión académica construida con ASP.NET Core 8, Entity Framework Core y MySQL 8.**

Proyecto de portafolio enfocado en la gestión de **personas, usuarios, estudiantes, roles, permisos y catálogos**. Incluye autenticación JWT, control de acceso basado en permisos (RBAC), migraciones de base de datos, Swagger y pruebas automatizadas.

> **Alcance:** backend para demostración local. No es un sistema institucional desplegado en producción. El archivo `modelo_relacional.sql` conserva un diseño histórico más amplio; no todas sus entidades o módulos están implementados.

## Tecnologías

| Área | Tecnología |
| --- | --- |
| API | C# · .NET 8 · ASP.NET Core Web API |
| Persistencia | Entity Framework Core · Pomelo · MySQL 8 |
| Seguridad | JWT Bearer · ASP.NET Core PasswordHasher · RBAC |
| Documentación HTTP | Swagger / OpenAPI 3 |
| Pruebas | xUnit · `dotnet test` |
| Desarrollo local | Windows PowerShell · Docker Compose (opcional) |

## Funcionalidades del MVP

- **Autenticación:** login con JWT y creación inicial de administrador solo en Development, protegida mediante un token de bootstrap.
- **Personas:** endpoints para crear, consultar, actualizar y eliminar registros.
- **Usuarios:** endpoints de gestión y contraseñas almacenadas con hash.
- **Estudiantes:** endpoints CRUD, relaciones con usuario y catálogos, claves foráneas e índices de unicidad.
- **Catálogos:** género, estado civil, tipo de identificación, tipo de formación y roles.
- **Autorización:** permisos por operación, asociaciones Usuario–Rol y Rol–Permiso.
- **Operación:** `/health`, `/health/ready`, manejo de errores HTTP, rate limiting y documentación Swagger en Development.

El repositorio contiene **solo el backend**; no incluye una interfaz web frontend.

## Estructura del repositorio

```text
WebApi/         Controladores, configuración HTTP, JWT y Swagger
Aplication/     Servicios y lógica de aplicación
Domain/         Entidades, DTO y códigos de permisos
Persistence/    DbContext, repositorios y migraciones EF Core
Tests/          Pruebas unitarias
scripts/        Herramientas de desarrollo, base de datos y pruebas
docs/           Documentación técnica y notas de migraciones
```

## Requisitos

- **.NET SDK 8** para compilar el proyecto `net8.0`. También se verificó la compilación local con SDK 10 instalado; la aplicación sigue apuntando a .NET 8.
- **MySQL 8** para las operaciones de persistencia.
- **Docker Desktop** opcional para alojar MySQL de manera aislada.
- **PowerShell** para los ejemplos en Windows.

## Inicio rápido (Windows + PowerShell)

Todos los comandos de esta sección se ejecutan **desde la raíz del repositorio**, salvo indicación contraria.

### 1. Preparar MySQL

Si ya existe el contenedor local `siga-ucc-dev-mysql` funcionando en `127.0.0.1:3307`, reutilízalo. No elimines su volumen.

Para crear el contenedor con Docker Compose por primera vez, configura `MYSQL_PASSWORD` y `MYSQL_ROOT_PASSWORD` como variables de la sesión **sin publicar sus valores** y luego ejecuta:

```powershell
docker compose up -d mysql
```

**Nota:** `compose.yaml` crea inicialmente la base `siga_ucc`. La demostración descrita aquí utiliza **otra base local**, `siga_ucc_demo`, que debe crearse y autorizarse para el usuario MySQL `siga` antes de migrar. Se puede realizar desde el cliente MySQL, conectado a ese mismo contenedor:

```sql
CREATE DATABASE IF NOT EXISTS siga_ucc_demo
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
GRANT ALL PRIVILEGES ON siga_ucc_demo.* TO 'siga'@'%';
```

No ejecutes estas instrucciones contra una base institucional o externa.

### 2. Configurar la conexión local

En PowerShell, introduce **la contraseña del usuario `siga`** (no la de `root`). El ejemplo supone una contraseña que no requiere escapado especial dentro de la cadena de conexión:

```powershell
$secure = Read-Host "Contraseña MySQL de siga" -AsSecureString
$password = [System.Net.NetworkCredential]::new("", $secure).Password
if ([string]::IsNullOrWhiteSpace($password)) { throw "Contraseña vacía" }

$env:ConnectionStrings__DefaultConnection = "Server=127.0.0.1;Port=3307;Database=siga_ucc_demo;User=siga;Password=$password"
Remove-Variable secure, password

$env:ASPNETCORE_ENVIRONMENT = "Development"
```

Las variables `$env:` solo se conservan en esta sesión y en los procesos que se lancen desde ella. Si la contraseña contiene caracteres especiales de sintaxis de conexión (por ejemplo `;`), utiliza una configuración de conexión adecuadamente escapada.

### 3. Configurar JWT de desarrollo

Genera una clave local criptográficamente aleatoria, compatible también con Windows PowerShell 5.1:

```powershell
$bytes = New-Object byte[] 64
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
try {
    $rng.GetBytes($bytes)
    $env:Jwt__SigningKey = [Convert]::ToBase64String($bytes)
}
finally {
    $rng.Dispose()
    [Array]::Clear($bytes, 0, $bytes.Length)
    Remove-Variable bytes, rng -ErrorAction SilentlyContinue
}
```

No copies la clave JWT, las contraseñas ni los tokens al repositorio, a incidencias o a capturas. También puedes usar `dotnet user-secrets` para persistir secretos **solo en tu equipo**; consulta `WebApi/appsettings.example.json`.

### 4. Aplicar migraciones en la base de demostración

**Comprueba antes** que la cadena de conexión apunta exclusivamente a `127.0.0.1:3307` / `siga_ucc_demo`. Este comando **modifica el esquema** de esa base local:

```powershell
dotnet ef migrations list --project .\Persistence\Persistence.csproj --startup-project .\WebApi\WebApi.csproj --context ApplicationDbContext

dotnet ef database update --project .\Persistence\Persistence.csproj --startup-project .\WebApi\WebApi.csproj --context ApplicationDbContext
```

Si `dotnet ef` no está disponible, instala la herramienta EF Core compatible con el proyecto. En la ejecución local documentada se aplicaron correctamente **10 migraciones**, incluida la de Estudiante y la de RBAC/unicidad de Persona, a `siga_ucc_demo`.

**Precaución:** la migración histórica `AgregarTablaPersona` no es segura sin una transición de datos para bases que ya contengan usuarios. Consulta [la estrategia de migraciones](docs/database/MIGRATION-RECOVERY.md). No ejecutes migraciones sobre bases institucionales.

### 5. Iniciar la API

En la misma sesión con las variables configuradas:

```powershell
dotnet run --project .\WebApi\WebApi.csproj
```

Durante la demostración local se utilizó `http://localhost:5175` con el perfil de lanzamiento del proyecto.

| Recurso | URL local |
| --- | --- |
| Health | [`/health`](http://localhost:5175/health) |
| Readiness MySQL | [`/health/ready`](http://localhost:5175/health/ready) |
| Swagger UI (Development) | [`/swagger/index.html`](http://localhost:5175/swagger/index.html) |

La ruta `/health/ready` valida conectividad con MySQL; un arranque de ASP.NET Core, por sí solo, no implica que la base esté disponible.

## Autenticación y demostración

La mayoría de los endpoints requieren `Authorization: Bearer <JWT>` y el permiso correspondiente. El flujo previsto es:

1. Preparar los catálogos `Genero` y `EstadoCivil` con datos ficticios válidos.
2. Configurar **antes de iniciar la API** `Bootstrap__Token` con un secreto aleatorio de al menos 32 caracteres, únicamente en Development.
3. Ejecutar una sola vez `POST /api/auth/bootstrap-admin` usando el encabezado `X-Bootstrap-Token` y un cuerpo con `nombreUsuario`, `contrasena` y `persona` (datos válidos y sus IDs de catálogo).
4. Autenticarse mediante `POST /api/auth/login`; la respuesta contiene `accessToken`.
5. Probar las operaciones autorizadas desde PowerShell o un cliente HTTP, enviando el JWT como encabezado Bearer.

El bootstrap inicial no es un registro público convencional. No habilites su token fuera del entorno Development y nunca publiques sus valores. Swagger documenta las rutas, pero la autenticación debe enviarse en los encabezados correspondientes.

## Pruebas

Desde la raíz:

```powershell
dotnet restore .\WebApi\WebApi.sln
dotnet build .\WebApi\WebApi.sln -c Release --no-restore
dotnet test .\WebApi\WebApi.sln -c Release --no-build
```

### Evidencia de validación local — 18 de noviembre de 2026

| Verificación | Resultado observado |
| --- | --- |
| Migraciones EF Core sobre `siga_ucc_demo` | **10/10 aplicadas** |
| `GET /health` | **Healthy** |
| `GET /health/ready` | **HTTP 200**, `{"status":"ready"}` |
| Swagger UI | **Accesible** |
| `GET /api/Persona` sin JWT | **401** |
| Login con credenciales inválidas | **401** |
| Bootstrap del administrador en Development | **Creación correcta**, usuario ID 1 |
| Login válido de administrador | **JWT obtenido** |
| GET autenticados de Persona, Usuario, Estudiante, Genero, EstadoCivil y permisos RBAC | **6/6 HTTP 200** |
| `POST /api/Persona` autenticado | **Persona creada**, ID 2 |
| `dotnet test .\WebApi\WebApi.sln -c Release` | **4/4 aprobadas**, 0 fallidas |

Estas comprobaciones acreditan un **MVP backend funcional en entorno local**. No se han verificado en esta sesión todas las operaciones CRUD, una denegación RBAC por falta de permisos (`403`) ni una suite HTTP E2E automatizada. No equivale a una certificación para producción.

## Documentación complementaria

- [Arranque en PowerShell](README-ARRANQUE-POWERSHELL.md) — guía histórica; algunas observaciones anteriores a la validación local pueden estar desactualizadas.
- [Recuperación y estrategia de migraciones](docs/database/MIGRATION-RECOVERY.md).
- [Guía de demostración para reclutadores](README-DEMO-RECLUTADORES.md) — puede contener resultados de auditorías anteriores.
- [Notas de pruebas y casos de uso](README-PRUEBAS-Y-CASOS-DE-USO.md).
- [Auditoría técnica](docs/audit/FINAL-ENTERPRISE-REPORT.md) — evidencia histórica, no refleja necesariamente los últimos cambios.

## Limitaciones conocidas

- El proyecto sigue en **.NET 8**; no se ha efectuado una migración validada a .NET 10.
- No hay frontend en este repositorio.
- El modelo relacional histórico es más amplio que el MVP implementado.
- Las migraciones históricas necesitan revisión específica para bases existentes con datos.
- La guía demuestra operaciones puntuales de la API, no exhaustividad de pruebas funcionales o autorización.

---

**Propósito:** proyecto académico/de portafolio, reproducible en desarrollo local y útil para explicar diseño de APIs, EF Core, MySQL, JWT y RBAC en entrevistas técnicas.
