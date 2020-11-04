using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcServer
{
    public class PricingService : Pricing.PricingBase
    {
        private readonly ILogger<GreeterService> _logger;
        public PricingService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task Subscribe(PriceRequest request, IServerStreamWriter<PriceResponse> responseStream, ServerCallContext context)
        {
            var i = 0;
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                var quote = $"Quote#{++i} for {request.Uic}-{request.AssetType}";
                _logger.Log(LogLevel.Information, quote);
                var response = new PriceResponse{Quote = quote};
                await responseStream.WriteAsync(response);
            }
        }
    }
}
