using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public class TagService : BaseDataService<Tag>, ITagService
    {
        public TagService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<Tag> FetchAsync(string key, CancellationToken cancellationToken = default)
        {
            using var context = _serviceProvider.Get<PlappDbContext>();

            return await context.Set<Tag>().FirstOrDefaultAsync(t => t.Key == key, cancellationToken);
        }
    }
}
