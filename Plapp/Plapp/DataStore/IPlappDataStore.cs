using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plapp
{
    public interface IPlappDataStore
    {
        Task<ITopicViewModel> CreateTopic();
        Task<ITagViewModel> CreateTag(string name);

        Task<IEnumerable<ITopicViewModel>> FetchTopicsAsync();
        Task<IEnumerable<IDataSeriesViewModel>> FetchDataSeries(int? topicId=null, int? tagId=null);
        Task<IEnumerable<INoteViewModel>> FetchNotes(int? topicId=null);
        Task<IEnumerable<ITagViewModel>> FetchTags();
        
        Task<int> SaveTopicAsync(ITopicViewModel topicViewModel);
        Task<int> SaveDataSeriesAsync(IDataSeriesViewModel dataSeriesViewModel);
        Task<int> SaveTag(ITagViewModel tagViewModel);
    }
}
