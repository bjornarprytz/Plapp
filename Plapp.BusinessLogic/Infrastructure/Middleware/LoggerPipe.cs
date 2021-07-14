using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Middleware
{
    public class LoggerPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {
        private readonly ILogger _logger;

        public LoggerPipe(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            _logger.LogInformation($"Handling request {request}");

            var result = await next();

            _logger.LogInformation($"Result of {request}: {result}");

            return result;
        }
    }
}
