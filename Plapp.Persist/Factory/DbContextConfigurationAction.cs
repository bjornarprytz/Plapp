using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xamarin.Essentials;

namespace Plapp.Persist
{
    public class DbContextConfigurationAction : IDbContextConfigurationAction
    {
        private readonly IConfiguration _configuration;

        public DbContextConfigurationAction(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public DbContextOptions<PlappDbContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<PlappDbContext>();

            var dbName = _configuration.GetConnectionString("PlappDb");

            var connStr = $"Data Source={Path.Combine(FileSystem.AppDataDirectory, dbName)}";

            options.UseSqlite(connStr);

            return options.Options; // TODO: Make sure this works, or does it have to be "Built"?
        }
    }
}