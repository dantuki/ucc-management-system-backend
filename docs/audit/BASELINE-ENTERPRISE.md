# Baseline Enterprise

## Estado observado

Rama de inicio: `codex/siga-ucc-backend-modernization` en `9f93215`. `main` era ancestro local. El SHA de referencia indicado en el encargo no coincidía con el `main` remoto previamente verificado (`46807c0`), por lo que no se asumió vigente.

| Severidad | Hallazgo confirmado | Estrategia |
|---|---|---|
| High | Catálogos sin autorización | `[Authorize]` en los cinco controladores de catálogo |
| High | AutoMapper 13.0.1 vulnerable | actualizado a 16.3.0 y build validado |
| High | Dependencias EF/Pomelo transitorias vulnerables | bloqueo documentado: evaluar proveedor compatible antes de salto a .NET 10 |
| High | Docker daemon no disponible | no ejecutar pruebas MySQL; scripts y compose aislado preparados |
| Medium | Puerto local 3306 conflictivo | compose usa 127.0.0.1 y 3307 configurable |
| Medium | Falta RBAC y modelo Usuario-Rol operativo | pendiente; no se simula con claims inventados |

Baseline de calidad posterior: build Release PASS, 2 pruebas unitarias PASS, integración/E2E/security HTTP NOT RUN o BLOCKED. Ninguna base externa fue contactada.
