# SIGA-UCC Backend

Backend .NET 8 para el MVP institucional de SIGA-UCC: catálogos, personas, estudiantes y usuarios. El modelo conceptual histórico permanece en `modelo_relacional.sql`; no todas sus tablas están implementadas.

## Requisitos

- .NET SDK 8.0.x
- MySQL 8 para ejecutar la API
- Docker Desktop opcional para una base local aislada

## Configuración segura

No copies secretos al repositorio. Parte de `WebApi/appsettings.example.json` y usa user-secrets:

```powershell
cd WebApi
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<cadena local>"
dotnet user-secrets set "Jwt:SigningKey" "<secreto aleatorio de al menos 64 bytes>"
```

Las credenciales que existían previamente en el repositorio deben rotarse antes de usar cualquier entorno compartido.

## Comandos

```powershell
dotnet restore WebApi/WebApi.sln
dotnet build WebApi/WebApi.sln -c Release --no-restore
dotnet test WebApi/WebApi.sln -c Release --no-build
dotnet run --project WebApi/WebApi.csproj
```

`GET /health` es público y Swagger existe solo en Development. `POST /api/auth/login` entrega JWT para usuarios con hash válido; las cuentas históricas con contraseña plana requieren reseteo administrativo.

## Límites actuales

No ejecutes migraciones sobre una base institucional. Revisa [docs/database/migrations.md](docs/database/migrations.md). La configuración EF de Estudiante requiere una migración revisada y validada en MySQL efímero. La solución sigue en .NET 8 hasta demostrar compatibilidad de EF Core y Pomelo con .NET 10.
