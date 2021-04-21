using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IPlappDataStore
    {
        Task<bool> EnsureStorageReadyAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Topic>> FetchTopicsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId=null, int? tagId=null, CancellationToken cancellationToken = default);
        Task<IEnumerable<DataPoint>> FetchDataPointsAsync(int dataSeriesId, CancellationToken cancellationToken = default);
        Task<Tag> FetchTagAsync(string tagKey, CancellationToken cancellationToken = default);
        Task<Tag> FetchTagAsync(int tagId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> FetchTagsAsync(CancellationToken cancellationToken = default);
        
        Task SaveTopicAsync(Topic topic, CancellationToken cancellationToken = default);
        Task SaveTopicsAsync(IEnumerable<Topic> topics, CancellationToken cancellationToken = default);
        Task SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries, CancellationToken cancellationToken = default);
        Task SaveDataSeriesAsync(DataSeries dataSeries, CancellationToken cancellationToken = default);
        Task SaveTagAsync(Tag tag, CancellationToken cancellationToken = default);

        Task DeleteTopicAsync(Topic topic, CancellationToken cancellationToken = default);
        Task DeleteDataSeriesAsync(DataSeries dataSeries, CancellationToken cancellationToken = default);
        Task DeleteDataPointAsync(DataPoint dataPoint, CancellationToken cancellationToken = default);
        Task DeleteTagAsync(Tag tag, CancellationToken cancellationToken = default);
    }
}
