[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
dotnet restore WebApi\WebApi.sln
dotnet build WebApi\WebApi.sln -c Release --no-restore
dotnet test WebApi\WebApi.sln -c Release --no-build
$audit = dotnet list WebApi\WebApi.sln package --vulnerable --include-transitive 2>&1
$audit | Write-Output
if ($audit -match '(?i)high|critical') { throw 'La auditoría de dependencias detectó vulnerabilidades altas o críticas.' }
