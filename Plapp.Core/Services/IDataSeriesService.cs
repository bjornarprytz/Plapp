using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IDataSeriesService : IDataService<DataSeries>
    {
        Task<IEnumerable<DataSeries>> FetchAllAsync(int? topicId = null, int? tagId = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<DataPoint>> FetchDataPointsAsync(int dataSeriesId, CancellationToken cancellationToken = default);
    }
}
