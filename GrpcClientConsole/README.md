# GRPC client demo

- [ ] Why is `Grpc.Tools` required ?



**Create a project**

```bash
dotnet new console -o GrpcClientConsole
cd GrpcClientConsole
dotnet new gitignore
```



**Add libraries**

```bash
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools
```

- [Grpc.Net.Client](https://www.nuget.org/packages/Grpc.Net.Client), which contains the .NET Core client.
- [Google.Protobuf](https://www.nuget.org/packages/Google.Protobuf/), which contains protobuf message APIs for C#.
- [Grpc.Tools](https://www.nuget.org/packages/Grpc.Tools/), which contains C# tooling support for protobuf files. The tooling package isn't required at runtime, so the dependency is marked with `PrivateAssets="All"`.



**Add the proto files for the API we want to connect with** 

```bash
mkdir Protos
cp <server-project>/Protos/greet.proto ./Protos/greet.proto 
```



We will modify the proto file to add this`option csharp_namespace = "GrpcClientConsole";`

```protobuf
option csharp_namespace = "GrpcClientConsole";
// ....
```

Modify the `csproj` file to add our proto file into it 

```xml
<ItemGroup>
  <Protobuf Include="Protos\greet.proto" GrpcServices="Client" />
</ItemGroup>
```



Now create the Program file as : 

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GrpcClientConsole
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Required to enable GRPC over HTTP (insecure)
      AppContext.SetSwitch(
        "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

      // The port number(5001) must match the port of the gRPC server.
      using var channel = GrpcChannel.ForAddress("http://localhost:5000");
      var client =  new Greeter.GreeterClient(channel);
      var reply = await client.SayHelloAsync(
              new HelloRequest { Name = "GreeterClient" });
      Console.WriteLine("Greeting: " + reply.Message);
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}
```

Read : https://github.com/grpc/grpc-dotnet/issues/505 to make gRPC work with HTTP.



Not runt the app

```bash
dotnet run                       
# Greeting: Hello GreeterClient
# Press any key to exit...
```



Now the packages that we added, will autogenerate our message classes in the namespace we gave above.



debugging: 

```
System.Reflection.Assembly.GetExecutingAssembly().GetTypes()

```



Resources: 

- https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-3.1&tabs=visual-studio-code