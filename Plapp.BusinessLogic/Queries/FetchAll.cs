using Plapp.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Queries
{
    public class FetchAll<T> : IRequestWrapper<IEnumerable<T>> 
        where T : DomainObject 
    {
        internal FetchAll() { }
    }

    public class GetAllHandler<T> : IHandlerWrapper<FetchAll<T>, IEnumerable<T>>
        where T : DomainObject
    {
        private readonly IDataService<T> _dataService;

        public GetAllHandler(IDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Response<IEnumerable<T>>> Handle(FetchAll<T> request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _dataService.FetchAllAsync(cancellationToken));
        }
    }
}
