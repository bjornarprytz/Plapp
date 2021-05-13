using Microsoft.EntityFrameworkCore;
using Plapp.Core;

namespace Plapp.Persist
{
    public class TopicService : BaseDataService<Topic>, ITopicService
    {
        public TopicService(IDbContextFactory<PlappDbContext> contextFactory) : base(contextFactory) { }
    }
}
