[CmdletBinding()]
param([string]$Urls = 'https://localhost:7080;http://localhost:5080')

$ErrorActionPreference = 'Stop'
if (-not $env:ConnectionStrings__DefaultConnection) { throw 'Configure ConnectionStrings__DefaultConnection mediante user-secrets o una variable de sesión.' }
if (-not $env:Jwt__SigningKey) { throw 'Configure Jwt__SigningKey fuera del repositorio.' }
$env:ASPNETCORE_URLS = $Urls
dotnet run --project WebApi\WebApi.csproj
