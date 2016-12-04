
BASE_SRC=src

# Workaround to Nuget issue: http://www.grpc.io/docs/quickstart/csharp.html
TOOLS_PATH=$(BASE_SRC)/packages/Grpc.Tools.1.0.0/tmp/packages/Grpc.Tools.1.0.0/tools/linux_x64
SERVER=$(BASE_SRC)/server
CLIENT=$(BASE_SRC)/client
SERVICE_CODEGEN=$(BASE_SRC)/Service

proto_codegen:
	$(TOOLS_PATH)/protoc -I$(BASE_SRC)/protos --csharp_out $(SERVICE_CODEGEN) --grpc_out $(SERVICE_CODEGEN) $(BASE_SRC)/protos/service.proto --plugin=protoc-gen-grpc=$(TOOLS_PATH)/grpc_csharp_plugin

build_svc: proto_codegen
	cd $(SERVICE_CODEGEN) && LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet restore &&	LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet build && cd ..

restore_server: build_svc
	cd $(SERVER) && LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet restore &&  cd ..

restore_client: build_svc
	cd $(CLIENT) && LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet restore &&  cd ..

run_server: restore_server
	cd $(SERVER) && LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet run && cd ..

run_client: restore_client
	cd $(CLIENT) && LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet run && cd ..
