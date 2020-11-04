using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcClientConsole
{
  class Program
  {
    static async Task Main(string[] args)
    {
      AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

      // The port number(5001) must match the port of the gRPC server.
      using var channel = GrpcChannel.ForAddress("http://host.docker.internal:50051");
      var client =  new GreetService.GreetServiceClient(channel);
      var greeting = new Greeting{FirstName = "Nishant", LastName = "Singh"};
      var request = new GreetManyTimesRequest{ Greeting = greeting };
      AsyncServerStreamingCall<GreetManyTimesResponse> result = client.GreetManyTimes(request);
      IAsyncStreamReader<GreetManyTimesResponse> streamReader = result.ResponseStream;

      bool hasMore = true;
      while (hasMore)
      {
        hasMore = await streamReader.MoveNext();
        GreetManyTimesResponse response = streamReader.Current;
        Console.WriteLine($"Response found : ${response.Result.ToString()}");
      }

      // var reply = await result;
      // Console.WriteLine("Greeting: " + reply.Message);
      // Console.WriteLine("Press any key to exit...");
      // Console.ReadKey();
    }
  }
}