dotnet tool restore
dotnet publish
dotnet rimraf -q ./artifacts > $null
dotnet zipper compress --input ./src/bin/Debug/netcoreapp3.1/publish/ --output ./artifacts/publish.zip
