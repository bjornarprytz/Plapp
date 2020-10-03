using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
                options.UseSqlite(construction.Configuration.GetConnectionString("PlappDataStoreConnection"));
            });

            construction.Services.AddScoped<IPlappDataStore>(
                provider => new PlappDataStore(provider.GetService<PlappDbContext>()));

            return construction;
        }
    }
}
