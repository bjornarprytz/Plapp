using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.DependencyInjection;

namespace Plapp.DependencyInjection
{
    public class PersistModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // DB
            services.AddSingleton<IDbContextFactory<PlappDbContext>, PlappDbContextFactory>();
            services.AddSingleton<IDbContextConfigurationAction, DbContextConfigurationAction>();
            
            // Services
            services.AddTransient<IDataSeriesService, DataSeriesService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ITopicService, TopicService>();
        }
    }
}