

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Plapp.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public class ValidationModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Plapp.Validation.AssemblyInfo>();
        }
    }
}