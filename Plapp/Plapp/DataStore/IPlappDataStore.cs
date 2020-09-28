using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plapp
{
    public interface IPlappDataStore
    {
        Task<ITopicViewModel> CreateTopicAsync();
        Task<ITagViewModel> CreateTagAsync(string tagId);

        Task<IEnumerable<ITopicViewModel>> FetchTopicsAsync();
        Task<IEnumerable<IDataSeriesViewModel>> FetchDataSeriesAsync(int? topicId=null, int? tagId=null);
        Task<IEnumerable<INoteViewModel>> FetchNotesAsync(int? topicId=null);
        Task<IEnumerable<ITagViewModel>> FetchTagsAsync();
        
        Task<int> SaveTopicAsync(ITopicViewModel topicViewModel);
        Task<int> SaveDataSeriesAsync(IEnumerable<IDataSeriesViewModel> dataSeriesViewModel);
        Task<bool> SaveTagAsync(ITagViewModel tagViewModel);
    }
}
