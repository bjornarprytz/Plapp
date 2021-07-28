using Plapp.Core;

namespace Plapp.BusinessLogic
{
    public interface IQueryFactory<T>
        where T : DomainObject
    {
        // Read operations

        FetchAll<T> CreateFetchAll();
        Fetch<T> CreateFetch(int id);
    }
}
