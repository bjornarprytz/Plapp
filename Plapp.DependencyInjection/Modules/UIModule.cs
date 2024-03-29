using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.UI;
using Plapp.UI.Infrastructure;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public class UIModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseMicrosoftDependencyResolver();

            services.AddSingleton<IPrompter, Prompter>();
        }
    }
}