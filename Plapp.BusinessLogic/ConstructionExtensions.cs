using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plapp.BusinessLogic.Middleware;

namespace Plapp.BusinessLogic
{
    public static class ConstructionExtensions
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {

            services.AddMediatR(typeof(AssemblyInfo).Assembly);

            // Order of pipes matter: first registered is first executed
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerPipe<,>));
        }
    }
}
