using Plapp.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public abstract class FetchAll<T> : IRequestWrapper<IEnumerable<T>> 
        where T : DomainObject 
    {
        protected FetchAll() { }
    }

    public class FetchAllTopics : FetchAll<Topic> { }

    public abstract class FetchAllHandler<T> : IHandlerWrapper<FetchAll<T>, IEnumerable<T>>
        where T : DomainObject
    {
        private readonly IDataService<T> _dataService;

        protected FetchAllHandler(IDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Response<IEnumerable<T>>> Handle(FetchAll<T> request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _dataService.FetchAllAsync(cancellationToken));
        }
    }

    public class FetchAllTopicsHandler : FetchAllHandler<Topic>
    {
        public FetchAllTopicsHandler(IDataService<Topic> dataService) : base(dataService) { }
    }
}
