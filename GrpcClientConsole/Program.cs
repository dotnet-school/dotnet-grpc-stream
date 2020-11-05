using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClientConsole
{
  class Program
  {
    static async Task Main(string[] args)
    {
      AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
      await SubscribeToPrices();
    }

    private static async Task SubscribeToPrices(string uic = "211", string assetType="Stock")
    {
      var request = new PriceRequest{Uic = uic, AssetType = assetType};
      
      Console.WriteLine("Connecting with grpc server");
      
      // using var channel = GrpcChannel.ForAddress("http://127.0.0.1:5000");
      using var channel = GrpcChannel.ForAddress("http://host.docker.internal:5000");

      var client = new Pricing.PricingClient(channel);
      
      Console.WriteLine("Requesting subcription");
      
      var streamReader = client.Subscribe(request).ResponseStream;
      while (await streamReader.MoveNext())
      {
        Console.WriteLine(streamReader.Current);
      }
      
      Console.WriteLine("Gracefully ended.");
    }
  }
}