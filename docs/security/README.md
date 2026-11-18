# Seguridad

- Se eliminó la cadena de conexión rastreada; use variables de entorno o user-secrets.
- Toda contraseña nueva usa `PasswordHasher<TUser>`; ningún DTO público incluye contraseña ni hash.
- Las cuentas con contraseña plana requieren reseteo administrado y auditable, no migración automática.
- Persona, Estudiante y Usuario requieren JWT. La clave se carga fuera del repositorio; Development puede usar una clave efímera.
- CORS está cerrado por defecto, hay rate limiting y errores con ProblemDetails.

RBAC y autorización por recurso siguen pendientes por falta de una relación Usuario-Rol operativa y requisitos institucionales aprobados. AutoMapper 13.0.1 conserva avisos de vulnerabilidad de NuGet y debe actualizarse junto con una validación de perfiles.
