using System;
using System.Threading;
using System.Threading.Tasks;

using Grpc.Core;

using Dd;

public static class Program {
    public static void Main(string[] args) {
        var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
        var client = new DictionaryService.DictionaryServiceClient(channel);

        var setResponse = client.Set(new SetRequest { Key = "foo", Value = "bar" });
        Console.WriteLine($"setResponse.Success={setResponse.Success}");

        var getResponse = client.Get(new GetRequest { Key = "foo" });
        Console.WriteLine($"getResponse.Value={getResponse.Value}");
        channel.ShutdownAsync().Wait();
        Console.WriteLine("gRPC client exiting");
    }
}
