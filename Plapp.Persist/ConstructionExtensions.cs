using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;

namespace Plapp.Persist
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction UsePlappDataStore(this FrameworkConstruction construction)
        {
            // Add filesystem

            construction.Services.AddDbContext<PlappDbContext>(options =>
            {
                options.UseSqlite("Data Source=PlappDb.db");
            }, contextLifetime: ServiceLifetime.Transient);

            construction.Services.AddScoped<IPlappDataStore>(
                provider => new PlappDataStore(provider.GetService<PlappDbContext>()));

            return construction;
        }
    }
}
