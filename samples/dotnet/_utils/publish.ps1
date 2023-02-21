dotnet tool restore
dotnet publish
dotnet rimraf -q ./artifacts > $null
dotnet zipper compress --input ./src/bin/Debug/net7.0/publish/ --output ./artifacts/publish.zip
