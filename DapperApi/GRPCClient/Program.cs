using DapperGRPC;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using Google.Protobuf;
using System.IO;

namespace GRPCClient
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.ReadKey(true);
      // The port number(5001) must match the port of the gRPC server.
      var channel = GrpcChannel.ForAddress("https://localhost:44355/");
      var client = new Greeter.GreeterClient(channel);
      var reply = await client.SayHelloAsync(
                        new HelloRequest { Name = "GreeterClient" });

      using (var output = File.Create(@"C:\vscode\github\TestRocket\DapperApi\src\DapperApi\GRPCClient\reply.dat"))
      {
        reply.WriteTo(output);
      }

      Console.WriteLine("Greetings Earthling: " + reply.Message);
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}
