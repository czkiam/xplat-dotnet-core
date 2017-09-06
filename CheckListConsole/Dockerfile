FROM microsoft/dotnet:1.1-runtime

WORKDIR /app
COPY bin/Release/netcoreapp1.1/publish .

ENTRYPOINT ["dotnet", "CheckListConsole.dll"]
