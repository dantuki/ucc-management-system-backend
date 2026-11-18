# Migraciones y riesgo de datos

No se ejecutó ninguna migración ni conexión a una base real.

La migración histórica `AgregarTablaPersona` añade `Usuario.id_persona` no nulo con valor por defecto. Con usuarios existentes puede no referenciar una Persona válida. Un despliegue seguro exige backup probado, inventario de usuarios sin Persona, backfill explícito, validación de integridad y solo después la restricción no nula. El rollback debe restaurar backup, no eliminar tablas de producción.

Estudiante ahora tiene claves foráneas e índices únicos en el modelo EF. Antes de crear su migración hay que comparar el esquema real, ejecutar el script generado contra MySQL efímero desde cero y probar datos representativos. No se modificaron migraciones históricas.
