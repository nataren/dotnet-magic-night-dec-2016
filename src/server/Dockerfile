FROM microsoft/dotnet:1.1.0-runtime
WORKDIR /app
ENV ASPNETCORE_URLS http://*:50051
EXPOSE 50051
ENTRYPOINT ["dotnet", "server.dll"]
COPY . /app
