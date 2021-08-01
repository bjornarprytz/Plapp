using Microsoft.EntityFrameworkCore;

namespace Plapp.Persist
{
    public interface IDbContextConfigurationAction
    {
        DbContextOptions<PlappDbContext> GetOptions();
    }
}