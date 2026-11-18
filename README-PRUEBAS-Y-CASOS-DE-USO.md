# Pruebas y casos de uso

| ID | Caso | Resultado real | Estado |
|---|---|---|---|
| DEMO-001 | Health | Endpoint configurado; no ejecutado contra API viva | NOT RUN |
| DEMO-002 | Readiness MySQL | Endpoint configurado; Docker daemon no disponible | BLOCKED |
| DEMO-003/004 | Login | Código presente; sin MySQL efímero | BLOCKED |
| DEMO-005 | Acceso sin token | Catálogos, Persona, Estudiante y Usuario tienen `[Authorize]`; sin prueba HTTP | NOT RUN |
| DEMO-007/008 | Persona y duplicado | Lógica y commit presentes; sin MySQL efímero | BLOCKED |
| DEMO-009/011 | Usuario/Estudiante | Lógica presente; sin MySQL efímero | BLOCKED |
| DEMO-012 | Estudiante inexistente | Controlador devuelve 404; sin prueba HTTP | NOT RUN |
| DEMO-018 | Unit tests | `dotnet test WebApi/WebApi.sln -c Release --no-build`: 2/2 | PASS |
| DEMO-019 | Integración | Docker daemon no disponible | BLOCKED |
| DEMO-020 | GitHub Actions | Workflow creado; no ejecutado en esta rama nueva | NOT RUN |

Los ejemplos de demostración y datos demo no se generan hasta disponer de una base MySQL efímera aislada y de un bootstrap administrativo sin credenciales predeterminadas.
