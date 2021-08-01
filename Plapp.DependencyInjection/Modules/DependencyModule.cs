using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public abstract class DependencyModule : IDependencyModule
    {
        public abstract void ConfigureServices(IServiceCollection services);

        public virtual void LoadConfiguration(IConfiguration configuration)
        {
            
        }
    }
}