using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;

namespace Plapp.Relational
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction UsePlappDatabase(this FrameworkConstruction construction)
        {
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
