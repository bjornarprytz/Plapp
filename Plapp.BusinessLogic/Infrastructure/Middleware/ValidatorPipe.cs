﻿using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public class ValidatorPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorPipe(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(v => v.Errors)
                .Where(e => e != null)
                .ToList();

            if (failures.Any())
            {
                return Task.FromResult(Response.Fail(failures.ToString()) as TResponse); // TODO: Fix this cast, it throws null exception
            }

            return next();
        }
    }
}
