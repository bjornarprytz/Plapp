

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Plapp.DependencyInjection;

namespace Plapp.Validation
{
    public class ValidationModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AssemblyInfo>();
        }
    }
}