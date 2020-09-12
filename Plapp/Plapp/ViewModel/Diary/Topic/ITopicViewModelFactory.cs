using System.Threading.Tasks;

namespace Plapp
{
    public interface ITopicViewModelFactory
    {
        Task<ITopicViewModel> Create();
    }
}
