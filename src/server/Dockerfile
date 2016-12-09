FROM microsoft/dotnet:1.1.0-runtime
WORKDIR /app
ENV GRPC_TRACE all
ENV GRPC_VERBOSITY DEBUG
EXPOSE 50051
ENTRYPOINT ["dotnet", "server.dll"]
COPY . /app
