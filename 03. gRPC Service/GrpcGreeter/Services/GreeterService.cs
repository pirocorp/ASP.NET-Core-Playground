namespace GrpcGreeter.Services
{
    using System.Threading.Tasks;

    using Grpc.Core;
    using Microsoft.Extensions.Logging;

    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            this._logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            this._logger.LogInformation("Saying hello to {Name}", request.Name);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
