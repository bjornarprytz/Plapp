using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IDataService<T> where T : DomainObject
    {
        Task<T> SaveAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SaveAllAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> FetchAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FetchAllAsync(CancellationToken cancellationToken = default);
    }
}
