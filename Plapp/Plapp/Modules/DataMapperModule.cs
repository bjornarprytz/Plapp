using Microsoft.Extensions.DependencyInjection;
using Plapp.DependencyInjection;

namespace Plapp.Modules
{
    public class DataMapperModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(PlappMapping.Configure()); // TODO: Configure in this class instead of PlappMapping
        }
    }
}