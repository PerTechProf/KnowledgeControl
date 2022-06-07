1. cd ClientApp
2. npm ci
3. cd ..
4. dotnet restore
5. (if "dotnet ef" doesn't work)dotnet tool install --global dotnet-ef
6. Copy appsettings.json, paste to same folder and rename new file to appsettings.Development.json
7. Add DbConnectionString to appsettings.Development.json
8. dotnet ef database update
9. dotnet run -v n
