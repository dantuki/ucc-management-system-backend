# Arquitectura

Actual: `WebApi -> Aplication -> Persistence -> Domain`, con WebApi como composition root. No es todavía Clean Architecture estricta porque Application depende de contratos alojados en Persistence. Se mantiene para evitar una migración masiva no verificada.

Los servicios confirman escrituras por `IUnitOfWork.CommitAsync`; EF Core no silencia conflictos; `ApplicationDbContext` declara `DbSet<Estudiante>`. La próxima iteración debe mover los contratos de persistencia a Application y renombrar `Aplication` de forma compatible.
