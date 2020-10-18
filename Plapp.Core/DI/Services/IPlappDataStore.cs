using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IPlappDataStore
    {
        Task<bool> EnsureStorageReadyAsync();

        Task<IEnumerable<Topic>> FetchTopicsAsync();
        Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId=null, string tagId=null);
        Task<IEnumerable<Note>> FetchNotesAsync(int? topicId=null);
        Task<Tag> FetchTagAsync(string tagId);
        Task<IEnumerable<Tag>> FetchTagsAsync();
        
        Task SaveTopicAsync(Topic topic);
        Task SaveTopicsAsync(IEnumerable<Topic> topics);
        Task SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries);
        Task SaveTagAsync(Tag tag);

        Task DeleteTopicAsync(Topic topic);
        Task DeleteDataSeriesAsync(DataSeries dataSeries);
        Task DeleteDataPointAsync(DataPoint dataPoint);
        Task DeleteTagAsync(Tag tag);


        Task<string> SaveFileAsync(string desiredName, Stream stream);
    }
}
