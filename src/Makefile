TOOLS_PATH=~/.nuget/packages/Grpc.Tools/1.0.1/tools/linux_x64

restore:
	LD_LIBRARY_PATH=~/oldicu:$(LD_LIBRARY_PATH) dotnet restore

codegen: restore
	$(TOOLS_PATH)/protoc --csharp_out . --grpc_out . service.proto --plugin=protoc-gen-grpc=$(TOOLS_PATH)/grpc_csharp_plugin

run: codegen
	LD_LIBRARY_PATH=~/oldicu/:$LD_LIBRARY_PATH dotnet run
