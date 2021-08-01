using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plapp.BusinessLogic;
using Plapp.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public class BusinessLogicModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(BusinessLogic.AssemblyInfo).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipe<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipe<,>));
        }
    }
}