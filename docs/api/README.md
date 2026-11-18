# API

Se conservan rutas legacy. Los recursos Persona y Estudiante inexistentes devuelven 404 y los duplicados generan ProblemDetails 409.

`UsuarioDto` es solo de lectura. Para crear use `UsuarioCreateRequestDto`; para actualizar use `UsuarioUpdateRequestDto`. Las rutas sensibles requieren `Authorization: Bearer <token>`. No existe alta pública de usuarios.
