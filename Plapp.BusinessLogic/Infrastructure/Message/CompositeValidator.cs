using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Plapp.BusinessLogic
{
    public class CompositeValidator<T> : ICompositeValidator<T>
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }
		
        public async Task<IEnumerable<ValidationResult>> ValidateAsync(T request, CancellationToken cancellationToken = default)
        {
            var context = new ValidationContext<T>(request);

            var errors = await Task.WhenAll(_validators
                .Select(v => v.ValidateAsync(context, cancellationToken)));
			
            return errors;
        }
    }

    public interface ICompositeValidator<in T>
    {
        Task<IEnumerable<ValidationResult>> ValidateAsync(T request, CancellationToken cancellationToken = default);
    }
}