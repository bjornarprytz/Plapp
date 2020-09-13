using Plapp.ViewModel.Topc;
using System.Threading.Tasks;

namespace Plapp
{
    public class TopicService : ITopicService
    {
        public TopicService()
        {

        }
        public async Task<ITopicViewModel> Create()
        {
            await Task.Delay(1000);

            return new TopicViewModel();
        }
    }
}
