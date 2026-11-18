# Informe de modernización

## Resumen y baseline

Rama: `codex/siga-ucc-backend-modernization`, creada desde `main` en `46807c0e7f09e84141539a70e1e4b79ac420d3eb`. El baseline restauró y compiló, con avisos NU1903 de AutoMapper y advertencias de nulabilidad; no había pruebas.

## Hallazgos y cambios

| Severidad | Hallazgo | Corrección |
|---|---|---|
| Critical | Cadena de conexión rastreada | Retirada, ejemplo sin secreto y guía user-secrets |
| High | Contraseñas asignadas directamente y devueltas por DTO | PasswordHasher, DTOs de escritura separados y JWT |
| High | Persona/Estudiante no hacían commit; Estudiante no estaba en EF | CommitAsync explícito, DbSet y configuración EF |
| High | Excepciones de concurrencia silenciadas | Se propagan; middleware las trata sin filtrar detalles |
| Medium | DI no registraba Persona/Estudiante | Registros centralizados en la extensión |
| Medium | Paginación podía no tener orden | El repositorio exige orden para paginar |

## Pruebas y CI

PASS: `dotnet build WebApi/WebApi.sln -c Release --no-restore`.

PASS: `dotnet test WebApi/WebApi.sln -c Release --no-build` (2 pruebas: hashing y ausencia de contraseña en DTO público).

BLOCKED: integración MySQL, migraciones en base temporal y tests de API. Docker Desktop no tenía daemon disponible; no se intentó conectar a una base externa. GitHub Actions ejecuta restore, build, pruebas y listado de paquetes vulnerables.

## Compatibilidad, deuda y decisión

Se conservaron rutas legacy; Usuario cambia deliberadamente a DTOs separados de entrada/salida. Pendientes prioritarios: actualizar AutoMapper con pruebas de perfiles; diseñar Usuario-Rol y autorización por recurso; generar y probar una migración de Estudiante con MySQL efímero; mover contratos de persistencia a Application; validar el upgrade .NET 10.

Decisión: **PARTIAL / READY FOR REVIEW**. No se alteraron migraciones históricas, datos reales ni `main`.
