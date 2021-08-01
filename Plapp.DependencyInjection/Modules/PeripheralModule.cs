using Microsoft.Extensions.DependencyInjection;
using Plapp.Peripherals;

namespace Plapp.DependencyInjection
{
    public class PeripheralModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICamera, Camera>();
        }
    }
}