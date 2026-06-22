# SIGA-UCC-BACKEND
 
Modelo Relacional :
Total tablas: 63
Total relaciones detectadas: 68

# RELACIONES

- ActividadesInvestigacion -> Proyecto
- ActividadesInvestigacion -> SemilleroInvestigacion
- ActividadesPeriodo -> ActividadesInvestigacion
- ActividadesPeriodo -> ActividadesPeriodoCursos
- ActividadesPeriodo -> CiicloPropedeuticoPeriodo
- ActividadesPeriodo -> Extension
- ActividadesPeriodo -> ProyeccionSocial
- Area -> ProfesorArea
- CategoriaMovilidad -> Movilidad
- CiicloPropedeutico -> CiicloPropedeuticoPeriodo
- Curso -> ActividadesPeriodoCursos
- Departamento -> ProfesorDepartamento
- EstadoCivil -> Persona
- Estudiante -> PlanEstudios
- Facultad -> ProfesorFacultad
- Genero -> Persona
- GrupoInvestigacion -> GruposSemilleroInvestigacion
- GrupoInvestigacion -> LineaGrupoInvestigacion
- GrupoInvestigacion -> ProfesorGrupoIvestigacion
- GrupoInvestigacion -> ProyectosGrupoInvestigacion
- LineaInvestigacion -> LineaGrupoInvestigacion
- LugarDesempeno -> LugarDesempenoPersona
- Modalidad -> ActividadesPeriodoCursos
- Modalidad -> Movilidad
- Modalidad -> PlanEstudios
- NucleoTipoCurso -> Curso
- PeriodoAcademico -> ActividadesPeriodo
- PeriodoAcademico -> Movilidad
- PeriodoAcademico -> TipoVinculacion
- Persona -> Estudiante
- Persona -> LugarDesempenoPersona
- Persona -> Movilidad
- Persona -> PersonaSemilleroInvestigacion
- Persona -> ProductoPersona
- Persona -> Profesor
- Persona -> RolProyectoPersona
- PlanEstudios -> Curso
- Producto -> ProductoPersona
- Profesor -> ActividadesPeriodo
- Profesor -> FormacionProfesional
- Profesor -> ProfesorArea
- Profesor -> ProfesorDepartamento
- Profesor -> ProfesorFacultad
- Profesor -> ProfesorGrupoIvestigacion
- ProgramaUniversitario -> LineaInvestigacion
- ProgramaUniversitario -> PlanEstudios
- Proyecto -> ProyectosGrupoInvestigacion
- Proyecto -> RolProyectoPersona
- Proyecto -> TrabajoGrado
- RolProfesor -> ActividadesPeriodo
- RolProyecto -> RolProyectoPersona
- SemilleroInvestigacion -> PersonaSemilleroInvestigacion
- TipoActividadesInvestigacion -> ActividadesInvestigacion
- TipoContratacion -> TipoVinculacion
- TipoCurso -> Curso
- TipoFormacion -> FormacionProfesional
- TipoFuente -> Producto
- TipoIdentificacion -> Persona
- TipoIndexacion -> CategoriaIndexacion
- TipoIndexacionProducto -> CategoriaIndexacion
- TipoIndexacionProducto -> Producto
- TipoModalidadGrado -> TrabajoGrado
- TipoMovilidad -> Movilidad
- TipoProyeccionSocial -> ProyeccionSocial
- TipoRecursoProyecto -> Proyecto
- Usuario -> Persona
- UsuarioRol -> Rol
- UsuarioRol -> Usuario

# TABLAS


## ActividadesInvestigacion
- PK | id_actividades_investigacion
- horas (int)
- FK | semillero_investigacion_id
- descripcion (str)
- FK | tipo_actividad_investigacion_id
- nombre (str)
- FK | actividad_periodo_id
- habilitado (bool)

## ActividadesPeriodo
- PK | id_actividades_periodo
- FK | periodo_academico_id
- FK | profesor_id
- FK | rol_profesor_id

## ActividadesPeriodoCursos
- PK | id_actividades_periodo
- FK | curso_id
- horas (int)
- FK | modalidad_id
- FK | actividad_periodo_curso_id

## Area
- PK | id_area
- nombre (str)
- habilitado (bool)

## CategoriaIndexacion
- PK | id_estudiante_recurso
- nombre (str)
- descripcion (str)
- FK | tipo_indexacion_id

## CategoriaMovilidad
- PK | id_categoria_movilidad
- nombre (str)
- habilitado (bool)

## CiicloPropedeutico
- PK | id_plan_estudios
- nombre (str)
- habilitado (bool)

## CiicloPropedeuticoPeriodo
- PK | id_plan_estudios
- FK | ciclo_propedeutico_id
- FK | actividad_periodo_id

## Curso
- PK | id_curso
- nombre (str)
- creditos (str)
- habilitado (bool)
- descripcion (str)

## Departamento
- PK | id_departamento
- nombre (str)
- habilitado (bool)

## EstadoCivil
- PK | id_estado_civil
- nombre (str)
- habilitado (bool)

## Estudiante
- PK | id_estudiante
- FK | persona_id
- id_numero_unico (int)
- FK | plan_estudios_id
- descripcion (str)

## Extension
- PK | id_extension
- nombre (str)
- descripcion (str)
- horas (int)
- FK | tipo_extension_id
- FK | actividad_periodo_id
- habilitado (bool)

## Facultad
- PK | id_facultad
- nombre (str)
- (bool)

## FormacionProfesional
- PK | id_formacion_profesional
- titulo_obtenido (str)
- pais_obtencion (str)
- nivel_formacion_profesional (int)
- FK | tipo_formacion_id
- path (str)
- path_one_drive (str)
- anio_graduacion (int)

## Genero
- PK | id_genero
- nombre (str)
- habilitado (bool)

## GrupoInvestigacion
- PK | id_grupo_investigacion
- nombre (str)
- codigo_minciencias (decimal)
- descripcion (str)

## GruposSemilleroInvestigacion
- PK | id__grupo_semillero_investigacion
- FK | grupo_investigacion_id
- FK | semillero_investigacion_id

## LineaGrupoInvestigacion
- PK | id_grupo_investigacion
- FK | linea_investigacion_id
- FK | grupo_investigacion_id

## LineaInvestigacion
- PK | id_programa_universitario
- nombre (str)
- FK | programa_universitario_id
- habilitado (bool)
- descripcion (str)

## LugarDesempeno
- PK | id_plan_estudios
- nombre (str)
- direccion (str)

## LugarDesempenoPersona
- PK | id_plan_estudios
- FK | lugar_desempeno_id
- FK | profesor_id
- horas (int)

## Modalidad
- PK | id_modalidad
- nombre (str)
- habilitado (bool)

## Movilidad
- PK | id_movilidad
- FK | tipo_movilidad_id
- FK | periodo_academico_id
- FK | categoria_movilidad_id
- entidad_origen (str)
- pais (str)
- objeto (str)
- resultados (str)
- FK | modalidad_id
- fecha_inicio (date)
- fecha_fin (date)
- duracion_dias (int)
- FK | persona_id

## NucleoTipoCurso
- PK | id_nucleo_tipo_curso
- nombre (str)
- descripcion (str)
- habilitado (bool)

## PeriodoAcademico
- PK | id_periodo_academico
- periodo (int)
- anio (int)
- fecha_inicial (date)
- fecha_final (date)

## Persona
- PK | id_persona
- FK | usuario_id
- primer_nombre (str)
- primer_apellido (str)
- fecha_nacimiento (date)
- direccion_residencia (str)
- email_personal (str)
- telefono (str)
- numero_identificacion (int)
- lugar_expedicion_documento (str)
- segundo_nombre (str)
- segundo_apellido (str)
- FK | genero_id
- FK | estado_civil_id
- correo_institucional (srt)
- estado (bool)
- ciudad_residencia (str)
- barrio_residencia (str)
- localidad_comuna_residencia (str)
- estrato (int)
- lugar_nacimiento (str)
- dpto_nacimiendo (str)

## PersonaSemilleroInvestigacion
- PK | id_estudiante_semillero_investigacion
- FK | persona_id
- FK | semillero_id
- habilitado (bool)
- responsable (bool)

## PlanEstudios
- PK | id_plan_estudios
- duracion (str)
- creditos (int)
- jornada (str)
- horario (str)
- titulo_otorga (str)
- FK | modalidad_id
- FK | programa_universitario_id

## Producto
- PK | id_producto
- fecha_cargue (date)
- titulo (str)
- descripcion (str)
- fecha_publicacion (date)
- resumen (str)
- path (str)
- identificador (str)
- numero_paginas (int)
- idioma (str)
- enlace_publicacion (str)

## ProductoPersona
- PK | id_producto_persona
- FK | persona_id
- FK | producto_id
- habilitado (bool)

## Profesor
- PK | id_profesor
- F | persona_id
- enlace_cvlac
- FK | grupo_investigacion_id
- categoria_investigador_minciencias
- categoria_escalafon_institucion
- profesor_id_unico (int)

## ProfesorArea
- PK | id_profesor_area
- profesor_id
- area_id

## ProfesorDepartamento
- PK | id_profesor_departamento
- profesor_id
- departamento_id

## ProfesorFacultad
- PK | id_profesor_facultad
- profesor_id
- facultad_id

## ProfesorGrupoIvestigacion
- PK | id_profesor_grupo_investigacion
- habilitado (bool)
- FK | profesor_id
- clasificación_investigador (str)
- responsable (bool)

## ProgramaUniversitario
- PK | id_programa_universitario
- nombre (str)
- habilitado (bool)
- descripcion (str)

## ProyeccionSocial
- PK | id_proyeccion_social
- nombre (str)
- descripcion (str)
- horas (int)
- FK | tipo_proyeccion_social_id
- FK | actividad_periodo_id
- habilitado (bool)

## Proyecto
- PK | id_proyectos
- nombre (str)
- anio (int)
- editorial (str) opcional
- presupuesto_proyecto (decimal)
- fecha_inicio_proyecto (date)

## ProyectosGrupoInvestigacion
- PK | id_grupo_investigacion
- FK | proyecto_id
- FK | grupo_investigacion_id

## Rol
- PK | id_rol  (int)
- nombre (str)
- fecha_creacion (date)
- habilitado (bool)
- fecha_modificacion (date)

## RolProfesor
- PK | id_rol_profesor
- nombre (str)
- habilitado (bool)

## RolProyecto
- PK | id_rol_proyecto
- nombre (str)
- habilitado(bool)

## RolProyectoPersona
- PK | id_rol_proyecto_persona
- FK | persona_id
- FK | proyecto_id
- FK | rol_proyecto_id

## SemilleroInvestigacion
- PK | id_semillero_investigacion
- nombre (str)
- descripcion (str)
- mision (str)
- vision (str)
- objetivos (str)

## TipoActividadesInvestigacion
- PK | id_tipo_actividades_investigacion
- nombre (str)
- habilitado (bool)

## TipoContratacion
- PK | id_tipo_contratacion
- nombre (str)
- habilitado (bool)
- descripcion (str)

## TipoCurso
- PK | id_tipo_curso
- nombre (str)
- descripcion (str)
- habilitado (bool)

## TipoExtension
- PK | id_tipo_proyeccion_social
- nombre (str)
- habilitado (bool)
- Briga

## TipoFormacion
- PK | id_tipo_formacion
- nombre (str)
- habilitado (bool)

## TipoFuente
- PK | id_estudiante_producto
- nombre (str)
- habilitado (bool)

## TipoIdentificacion
- PK | id_tipo_identificacion
- nombre (str)
- habilitado (bool)
- abreviatura (str)

## TipoIndexacion
- PK | id_tipo_indexacion
- nombre (str)
- habilitado (bool)
- descripcion (str)
- usuario_actualizacion (str)
- fecha_actualizacion (date)
- usuario_creacion (str)
- fecha_creacion (date)
- usuario_eliminacion(str)
- fecha_eliminacion(date)

## TipoIndexacionProducto
- PK | id_tipo_indexacion_producto
- FK | producto_id
- FK | categoria_indexacion_id

## TipoModalidadGrado
- PK | id_tipo_modalidad
- nombre (str)
- FK | habilitado (bool)

## TipoMovilidad
- PK | id_tipo_movilidad
- nombre (str)
- habilitado (bool)

## TipoProducto
- PK | id_estudiante_producto
- nombre (str)
- habilitado (bool)

## TipoProyeccionSocial
- PK | id_tipo_proyeccion_social
- nombre (str)
- habilitado (bool)

## TipoRecursoProyecto
- PK | id_tipo_recurso_proyecto
- nombre (str)
- habilitado(bool)

## TipoVinculacion
- PK | id_tipo_vinculacion
- FK | periodo_academico_id
- nombre (str)
- FK | tipo_contratacion_id
- descripcion (str)

## TrabajoGrado
- PK | id_trabajo_grado_estudiante
- titulo (str)
- FK | proyecto_id (opcional)
- FK | tipo_modadlidad_grado_id

## Usuario
- PK | id_usuario
- nombre_usuario (str)
- contrasena (str)
- habilitado (bool)
- fecha_creacion (date)
- fecha_modificacion (date)

## UsuarioRol
- PK | id_usuariorol
- FK | rol_id
- FK | usuario_id
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa