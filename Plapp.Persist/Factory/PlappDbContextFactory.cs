using Microsoft.EntityFrameworkCore;
using System;

namespace Plapp.Persist
{
    public class PlappDbContextFactory : IDbContextFactory<PlappDbContext>
    {
        private readonly IDbContextConfigurationAction _configureDbContext;

        public PlappDbContextFactory(IDbContextConfigurationAction configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public PlappDbContext CreateDbContext()
        {
            return new PlappDbContext(_configureDbContext.GetOptions());
        }
    }
}
