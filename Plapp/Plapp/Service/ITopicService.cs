using System.Threading.Tasks;

namespace Plapp
{
    public interface ITopicService
    {
        Task<ITopicViewModel> Create();
    }
}
