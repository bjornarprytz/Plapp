using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.UI.ContentViews;
using Plapp.UI.Pages;
using ReactiveUI;
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