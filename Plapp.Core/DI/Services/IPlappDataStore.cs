﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IPlappDataStore
    {
        Task<bool> EnsureStorageReadyAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Topic>> FetchTopicsAsync();
        Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId=null, string tagKey=null, string dataSeriesTitle=null);
        Task<IEnumerable<DataPoint>> FetchDataPointsAsync(int dataSeriesId);
        Task<Tag> FetchTagAsync(string tagKey);
        Task<IEnumerable<Tag>> FetchTagsAsync();
        
        Task SaveTopicAsync(Topic topic);
        Task SaveTopicsAsync(IEnumerable<Topic> topics);
        Task SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries);
        Task SaveDataSeriesAsync(DataSeries dataSeries);
        Task SaveTagAsync(Tag tag);

        Task DeleteTopicAsync(Topic topic);
        Task DeleteDataSeriesAsync(DataSeries dataSeries);
        Task DeleteDataPointAsync(DataPoint dataPoint);
        Task DeleteTagAsync(Tag tag);
    }
}
