using Microsoft.EntityFrameworkCore;
using System;

namespace Plapp.Persist
{
    public class PlappDbContextFactory : IDbContextFactory<PlappDbContext>
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public PlappDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public PlappDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<PlappDbContext>();

            _configureDbContext(options);

            return new PlappDbContext(options.Options);
        }
    }
}
