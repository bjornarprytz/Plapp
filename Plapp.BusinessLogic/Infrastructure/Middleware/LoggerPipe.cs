using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
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
            _logger.LogInformation($"Handling {typeof(TIn)}");

            var result = await next();

            if (result is IResponseWrapper { IsError: true } r)
            {
                _logger.LogError($"Request of type {typeof(TIn)} failed:{Environment.NewLine}{r.Failures.Aggregate((a, b) => a + Environment.NewLine + b)}");
            }

            return result;
        }
    }
}
