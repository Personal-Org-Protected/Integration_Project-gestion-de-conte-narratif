param (
    $filePath,
    $connectionString
)

$pathToJson = "${filePath}"
$a = Get-Content $pathToJson | ConvertFrom-Json
$a.ConnectionStrings.ConnectionSecurity = "${connectionString}"
$a | ConvertTo-Json | set-content $pathToJson

Write-Host "setting changed"