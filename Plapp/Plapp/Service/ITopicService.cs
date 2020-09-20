using System.Threading.Tasks;

namespace Plapp
{
    public interface ITopicService
    {
        Task<ITopicViewModel> Create();
        Task<ITopicMetaDataViewModel> CreateMetaData();

        Task Save(ITopicViewModel topic);
    }
}
