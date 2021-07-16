using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plapp.BusinessLogic.Middleware;
using Plapp.Core;
using System.Collections.Generic;

namespace Plapp.BusinessLogic
{
    public static class ConstructionExtensions
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyInfo).Assembly);

            services.AddTransient(typeof(IRequestHandler<FetchAll<Topic>, Response<IEnumerable<Topic>>>), typeof(FetchAllTopicsHandler));

            services.AddSingleton<IQueryFactory<Topic>, QueryFactory<Topic>>();
            services.AddSingleton<ICommandFactory<Topic> , CommandFactory <Topic>>();

            // Order of pipes matter: first registered is first executed
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerPipe<,>));
        }
    }
}
