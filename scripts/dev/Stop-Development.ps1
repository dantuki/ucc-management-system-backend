[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
docker compose stop mysql
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
Write-Host 'MySQL detenido. El volumen de desarrollo se conserva.'
