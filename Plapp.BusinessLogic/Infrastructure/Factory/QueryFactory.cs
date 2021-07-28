using Plapp.Core;
using System;

namespace Plapp.BusinessLogic
{
    public class QueryFactory<T> : IQueryFactory<T>
        where T : DomainObject
    {
        public Fetch<T> CreateFetch(int id)
        {
            return new Fetch<T>(id);
        }

        public FetchAll<T> CreateFetchAll()
        {
            throw new NotImplementedException();

            //return new FetchAll<T>();
        }
    }
}
