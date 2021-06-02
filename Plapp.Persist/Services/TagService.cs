using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public class TagService : BaseDataService<Tag>, ITagService
    {
        public TagService(IDbContextFactory<PlappDbContext> contextFactory) : base(contextFactory) { }

        public async Task<Tag> FetchAsync(string key, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<Tag>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Key == key, cancellationToken);
        }
    }
}
