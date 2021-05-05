using Plapp.Core;
using System;

namespace Plapp.Persist
{
    public class TopicService : BaseDataService<Topic>, ITopicService
    {
        public TopicService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
