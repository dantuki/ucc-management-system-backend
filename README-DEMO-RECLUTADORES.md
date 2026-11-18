# Demo para reclutadores

## Pitch de 30 segundos

SIGA-UCC es un backend .NET para el MVP de gestión académica. Separé contratos HTTP de entidades, eliminé la exposición de contraseñas, añadí hashing, JWT, manejo uniforme de errores, rate limiting y persistencia explícita para Persona y Estudiante. La solución compila y sus pruebas unitarias verifican hashing y la no exposición de secretos.

## Recorrido de 5 minutos

1. Muestre `WebApi/Program.cs`: autenticación, rate limiting, health y ProblemDetails.
2. Muestre `Aplication/Services/Security/PasswordHashingService.cs`.
3. Ejecute `dotnet build WebApi/WebApi.sln -c Release --no-restore` y `dotnet test WebApi/WebApi.sln -c Release --no-build`.
4. Muestre `docs/audit/FINAL-ENTERPRISE-REPORT.md` para los límites: Docker/MySQL, RBAC y E2E aún no validados.

## Preguntas técnicas basadas en el código

- **¿Por qué PasswordHasher?** `PasswordHashingService` delega en `PasswordHasher<Usuario>`, no almacena contraseñas planas.
- **¿401 y 403?** JWT no válido o ausente se resuelve en la autenticación; las policies/RBAC todavía no existen, por lo que no se afirma un 403 validado.
- **¿Qué hace EF Core?** `ApplicationDbContext` mapea entidades y `CommitAsync` persiste; `EstudianteConfig` declara sus índices y FKs.
- **¿Qué deuda queda?** `docs/audit/FINAL-ENTERPRISE-REPORT.md` enumera vulnerabilidades transitivas, falta de daemon Docker, RBAC y migración de Estudiante.

No afirme producción lista: la evidencia actual demuestra build y dos pruebas unitarias, no un recorrido MySQL E2E.

## 25 preguntas para entrevista

1. **¿DI?** `DependencyInjection.AddInterfacesInjection` registra servicios y repositorios por scope.
2. **¿JWT?** `JwtTokenService` emite issuer, audience, expiración e identidad; `Program.cs` valida los cuatro.
3. **¿401 vs 403?** 401 es falta/fallo de autenticación; 403 requiere una policy que el modelo actual aún no implementa.
4. **¿Por qué PasswordHasher?** `PasswordHashingService` usa un algoritmo de password hashing mantenido por ASP.NET.
5. **¿EF Core?** `ApplicationDbContext` representa el modelo y `CommitAsync` aplica el unit of work.
6. **¿IQueryable vs IEnumerable?** `BaseRepository` construye consultas IQueryable y materializa listas solo al final.
7. **¿AsNoTracking?** El repositorio lo usa para lecturas para no mantener entidades innecesariamente rastreadas.
8. **¿Migraciones?** Versionan esquema; `docs/database/migrations.md` prohíbe aplicarlas a producción sin plan.
9. **¿Transacción?** EF agrupa un `SaveChangesAsync`; el contexto ya no oculta excepciones de concurrencia.
10. **¿FK?** `EstudianteConfig` declara claves a catálogos y Usuario con borrado restrictivo.
11. **¿Uno a uno?** Usuario-Persona se mapea como una relación uno a uno en el contexto.
12. **¿Uno a muchos?** Catálogos son referenciados por múltiples registros, definidos con `WithMany`.
13. **¿Por qué capas?** Domain modela; Persistence implementa EF; WebApi compone dependencias.
14. **¿Clean Architecture completa?** No: Application aún consume contratos de Persistence; está documentado como deuda.
15. **¿SOLID?** Servicios separan casos de uso de controladores, aunque la abstracción de repositorio requiere simplificación futura.
16. **¿SQL injection?** Se usan expresiones EF, no concatenación de SQL de entrada.
17. **¿RBAC?** Es pendiente; no se inventaron roles/claims sin un modelo Usuario-Rol persistente.
18. **¿Duplicados?** Persona y Estudiante validan antes de confirmar y el modelo define índices de Estudiante.
19. **¿Concurrencia?** `CommitAsync` propaga conflicto para que no se informe éxito falso.
20. **¿MySQL desconectado?** `/health/ready` devuelve 503 y los logs no exponen la cadena de conexión.
21. **¿Integración?** Debe usar MySQL efímero; el daemon ausente bloqueó esa validación.
22. **¿Cobertura?** No se reporta aún; dos pruebas unitarias no justifican una métrica amplia.
23. **¿CI?** `.github/workflows/ci.yml` restaura, compila, prueba y audita paquetes.
24. **¿Secretos?** `appsettings.example.json` tiene placeholders; los valores van a user-secrets o ambiente.
25. **¿Deuda?** `FINAL-ENTERPRISE-REPORT.md` lista dependencias vulnerables, RBAC y pruebas MySQL/E2E.
