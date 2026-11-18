[CmdletBinding()]
param([int]$Port = 3307)

$ErrorActionPreference = 'Stop'
if (-not (Get-Command docker -ErrorAction SilentlyContinue)) { throw 'Docker Desktop no está instalado o no está en PATH.' }
docker info *> $null
if ($LASTEXITCODE -ne 0) { throw 'Docker Desktop no está iniciado o no permite conexión.' }
if (-not $env:MYSQL_PASSWORD -or -not $env:MYSQL_ROOT_PASSWORD) { throw 'Defina MYSQL_PASSWORD y MYSQL_ROOT_PASSWORD solo en esta sesión antes de iniciar MySQL.' }
$env:MYSQL_PORT = $Port
docker compose up -d mysql
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
docker compose ps mysql
