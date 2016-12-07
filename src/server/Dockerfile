FROM microsoft/dotnet:1.0.0-core
WORKDIR /app
ENV ASPNETCORE_URLS http://*:50051
EXPOSE 50051
ENTRYPOINT ["dotnet", "server.dll"]
COPY . /app
