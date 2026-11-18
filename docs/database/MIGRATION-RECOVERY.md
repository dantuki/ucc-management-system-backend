# Recuperación de migraciones MySQL

## Alcance y aislamiento

La recuperación se verificó en MySQL `8.0.46` local, en un contenedor temporal llamado `siga-ucc-migration-verify-mysql`, puerto `127.0.0.1:3309` y volumen dedicado `siga-ucc-migration-verify-data`.

No se ejecutó ninguna migración contra `siga_ucc`, el contenedor `siga-ucc-dev-mysql`, su volumen `siga-ucc-dev-mysql-data`, PostgreSQL ni una base institucional. El script nunca elimina bases ni volúmenes y se niega a reutilizar un contenedor temporal existente.

## Hallazgos del historial

Hay ocho migraciones históricas. No se han reescrito.

- `20250307011716_pruebaDbo` renombra `Rol` a la tabla literal `admin.Rol`. En MySQL no representa un esquema `admin`; el punto forma parte del nombre. El modelo vigente conserva ese mapeo para que no exista deriva.
- `20250305003054_editTableEstadoCivil` amplía `Descripcion` y `20260618144115_RemoveDescripcionFromEstadoCivil` finalmente la elimina. La secuencia fue ejecutada con éxito en una instalación vacía.
- `20260622145549_AgregarTablaPersona` agrega `Usuario.id_persona` como obligatorio con valor por defecto `0` antes de crear su FK. Esto funciona cuando `Usuario` está vacío, pero una instalación que ya tenga usuarios fallará al añadir la FK porque no existe `Persona(0)` y, además, el índice único no admite varios valores `0`.

La migración nueva `20261118200924_AddEstudianteAndPersonaIdentityIndexes` es aditiva: crea `Estudiante`, sus cuatro FKs y los índices únicos de código, documento y usuario. No cambia datos existentes ni modifica migraciones previas.

## Estrategia de despliegue

### Instalaciones vacías

Aplicar el historial completo mediante EF Core. La validación real completó las nueve migraciones, incluida la nueva de `Estudiante`.

### Bases con datos

No ejecutar a ciegas `20260622145549_AgregarTablaPersona`. Antes de la ventana de mantenimiento, un responsable de datos debe:

1. Tomar backup comprobable y ensayar el proceso sobre una copia restaurada.
2. Inventariar `Usuario` y definir el mapeo de cada usuario a una `Persona` válida; no se debe crear una persona ficticia con id `0`.
3. Aplicar un plan de transición específico y transaccional que cree las personas, rellene `Usuario.id_persona`, compruebe unicidad y solo después añada `NOT NULL`, el índice único y la FK.
4. Registrar de forma coherente el estado en `__EFMigrationsHistory` bajo revisión de DBA. Si una base tiene tablas manuales o un historial parcial, debe reconciliarse primero; no se debe editar ni insertar el historial de producción como atajo.
5. Tras las ocho migraciones históricas confirmadas, la migración de `Estudiante` puede aplicarse como cambio aditivo, previa comprobación de que no exista una tabla homónima incompatible.

La relación validada es `Estudiante.id_usuario -> Usuario.id_persona -> Persona.id_persona`. `Estudiante` repite datos de identidad de `Persona`; no hay FK directa entre ambas y esa posible divergencia queda como riesgo de diseño pendiente, fuera de esta recuperación de esquema.

## Ejecución local aislada

Desde la raíz del repositorio, generar una contraseña efímera en memoria y ejecutar:

```powershell
$migrationPassword = ([guid]::NewGuid().ToString('N') + 'aA1!')
.\scripts\database\Test-MigrationRecovery.ps1 -DatabasePassword $migrationPassword
```

El secreto no se escribe en archivos versionados ni se imprime. Se requiere Docker Desktop y la herramienta `dotnet ef` disponible mediante las dependencias del proyecto.

## Evidencia de ejecución (2026-11-18)

| Comprobación | Resultado |
| --- | --- |
| MySQL temporal healthy | PASS |
| Ocho migraciones históricas | PASS |
| `AddEstudianteAndPersonaIdentityIndexes` | PASS |
| Segunda ejecución de `database update` | PASS: sin migraciones pendientes |
| Tablas esperadas, incluida `admin.Rol` | PASS |
| Inserción Persona → Usuario → Estudiante | PASS |
| Consulta de relación encadenada | PASS |
| 4 FKs de Estudiante | PASS |
| 3 índices únicos de Estudiante | PASS |
| Duplicado de código, persona de usuario y FK inválida | PASS: rechazados por MySQL |
| Entorno habitual `siga-ucc-dev-mysql` | PASS: no modificado |

## Riesgos pendientes

- La transición histórica de `Usuario` a `Persona` requiere un plan por datos para cualquier instalación no vacía.
- El nombre literal `admin.Rol` es una decisión histórica incompatible con la expectativa de un esquema MySQL. Corregirlo exigiría una migración posterior, coordinada y compatible.
- La duplicación de datos de identidad entre Persona y Estudiante requiere una decisión de modelo antes de una refactorización futura.
- Esta prueba no aplica ni sustituye una validación sobre una copia anonimizada de producción.
