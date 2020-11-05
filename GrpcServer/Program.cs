using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace GrpcServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        private  static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                            .ConfigureWebHostDefaults(getConfiguration());
        }


        private static Action<IWebHostBuilder> configureForMacos = webBuilder =>
        {
            webBuilder.ConfigureKestrel(options =>
            {
                // Setup a HTTP/2 endpoint without TLS.
                options.ListenLocalhost(5000, o => o.Protocols =
                                HttpProtocols.Http2);
            });
            webBuilder.UseStartup<Startup>();
        };

        private static Action<IWebHostBuilder> conigure = webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        };
        
        private static Action<IWebHostBuilder> getConfiguration()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? configureForMacos : conigure;
        }
    }
}
