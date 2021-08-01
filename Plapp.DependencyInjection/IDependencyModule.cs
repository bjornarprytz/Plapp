using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public interface IDependencyModule
    {
        void ConfigureServices(IServiceCollection services);
        void LoadConfiguration(IConfiguration configuration);
    }
}