using System;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

using Grpc.Core;

using Dd;

public class DictionaryServiceImpl : DictionaryService.DictionaryServiceBase {

    // Fields
    private static Dictionary<string, string> _dict = new Dictionary<string, string>(10000);

    // Methods
    public override Task<GetResponse> Get(GetRequest request, ServerCallContext context) {
        string value;
        _dict.TryGetValue(request.Key, out value);
        return Task.FromResult(new GetResponse { Value = value });
    }

    public override Task<SetResponse> Set(SetRequest request, ServerCallContext context) {
        _dict[request.Key] = request.Value;
        return Task.FromResult(new SetResponse { Success = true });
    }
}

public class Program {
    private static AutoResetEvent autoEvent = new AutoResetEvent(false);

    public static void Main(string[] args) {
        Console.WriteLine("Firing off gRPC server");
        const int port = 50051;
        var server = new Server {
            Services = { DictionaryService.BindService(new DictionaryServiceImpl ()) },
            Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
        };

        ThreadPool.QueueUserWorkItem(_ => {
                server.Start();
                Console.WriteLine($"server listening on port {port}");
            },
            autoEvent
        );
        autoEvent.WaitOne();

        // Console.ReadLine or Console.ReadKey works outside of the container, not on the container though :-(
        server.ShutdownAsync().Wait();
        Console.WriteLine("Shutting down ...");
    }
}
