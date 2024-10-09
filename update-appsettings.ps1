param (
    $filePath,
    $connectionString
)

Write-Host "Change of setting process...."
$pathToJson = "C:/Users\MAICQ97/source/Powershell Scripts/appsettings.json"
$a = Get-Content $pathToJson | ConvertFrom-Json
$a.ConnectionStrings.ConnectionSecurity = "test"
$a | ConvertTo-Json | set-content $pathToJson

Write-Host $a.ConnectionStrings.ConnectionSecurity
Write-Host "setting changed"