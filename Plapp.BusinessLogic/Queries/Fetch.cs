using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public class Fetch<T> : IRequestWrapper<T>
        where T : DomainObject
    {
        public int Id { get; private set; }
        internal Fetch(int id)
        {
            Id = id;
        }
    }

    public class FetchHandler<T> : IHandlerWrapper<Fetch<T>, T>
        where T : DomainObject
    {
        private readonly IDataService<T> _dataService;

        public FetchHandler(IDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Response<T>> Handle(Fetch<T> request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _dataService.FetchAsync(request.Id, cancellationToken));
        }
    }
}
