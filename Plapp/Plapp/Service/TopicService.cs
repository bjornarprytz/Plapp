
using System.Threading.Tasks;

namespace Plapp
{
    public class TopicService : ITopicService
    {
        public async Task<ITopicViewModel> Create()
        {
            return IoC.Get<ITopicViewModel>();
        }

        public async Task<ITopicMetaDataViewModel> CreateMetaData()
        {
            return await Task.FromResult(new TopicMetaDataViewModel());
        }

        public Task Save(ITopicViewModel topic)
        {
            throw new System.NotImplementedException();
        }
    }
}
