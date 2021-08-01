using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plapp.DependencyInjection;

namespace Plapp.BusinessLogic
{
    public class BusinessLogicModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyInfo).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipe<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipe<,>));
            
            throw new System.NotImplementedException();
        }
    }
}