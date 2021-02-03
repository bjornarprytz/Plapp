using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PCLStorage;
using Plapp.Core;

namespace Plapp.Persist
{
    public static class ConstructionExtensions
    {

        public static FrameworkConstruction AddPlappDataStore(this FrameworkConstruction construction)
        {
            var connStr = $"Data Source={FileSystem.Current.LocalStorage.Path}/Plapp.db"; // TODO: Move this to config

            construction.Services.AddSingleton(provider =>
            {
                return FileSystem.Current;
            });

            construction.Services.AddDbContext<PlappDbContext>(options =>
            {
                options.UseSqlite(connStr);
            }, contextLifetime: ServiceLifetime.Transient);

            construction.Services.AddTransient<IPlappDataStore>(
                provider => new PlappDataStore(
                    provider.GetService<PlappDbContext>(), 
                    provider.GetService<IFileSystem>()));

            return construction;
        }
    }
}
