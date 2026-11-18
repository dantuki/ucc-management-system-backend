[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
if ($env:SIGA_UCC_ALLOW_LOCAL_MIGRATIONS -ne 'true') { throw 'Por seguridad, defina SIGA_UCC_ALLOW_LOCAL_MIGRATIONS=true para habilitar migraciones locales.' }
if ($env:ConnectionStrings__DefaultConnection -notmatch '(?i)Database=siga_ucc_demo') { throw 'La conexión debe apuntar exclusivamente a la base local siga_ucc_demo.' }
dotnet ef migrations list --project Persistence\Persistence.csproj --startup-project WebApi\WebApi.csproj
dotnet ef database update --project Persistence\Persistence.csproj --startup-project WebApi\WebApi.csproj
