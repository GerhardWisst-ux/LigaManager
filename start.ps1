Write-Host "🔧 Starte Dienste..."
docker-compose up -d --build

Write-Host "⏳ Warte auf SQL Server..."
Start-Sleep -Seconds 15

Write-Host "📁 Kopiere setup.sql in den SQL Server Container..."
docker cp .\setup.sql db:/setup.sql

Write-Host "📦 Führe SQL-Skript aus..."
docker exec -i db /opt/mssql-tools/bin/sqlcmd `
    -S localhost `
    -U sa `
    -P "YourStrong!Passw0rd" `
    -i /setup.sql