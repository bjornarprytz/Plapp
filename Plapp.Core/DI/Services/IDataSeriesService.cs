using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IDataSeriesService : IDataService<DataSeries>
    {
        Task<IEnumerable<DataSeries>> FetchAsync(int? topicId = null, int? tagId = null, CancellationToken cancellationToken = default);
    }
}
