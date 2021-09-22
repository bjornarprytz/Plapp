using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface ITagService : IDataService<Tag>
    {
        Task<Tag> FetchAsync(string key, CancellationToken cancellationToken = default);
    }
}
