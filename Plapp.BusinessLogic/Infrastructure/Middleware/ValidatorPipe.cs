using System;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public class ValidatorPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : IResponseWrapper
    {
        private readonly ICompositeValidator<TRequest> _validator;

        public ValidatorPipe(ICompositeValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var results = await _validator.ValidateAsync(request, cancellationToken);
            
            var failures = results.SelectMany(v => v.Errors)
                .Where(e => e != null)
                .Select(f => f.ErrorMessage)
                .ToList();

            if (failures.Any())
            {
                return Response.GenerateTypedErrorResponse<TResponse>(failures);
            }

            return await next();
        }
        
        
    }
}
