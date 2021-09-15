using Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public class UIModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseMicrosoftDependencyResolver();
        }
    }
}