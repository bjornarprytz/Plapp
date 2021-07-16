using MediatR;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public class SaveAllCommand<T> : IRequestWrapper<Unit>
    {
        internal SaveAllCommand(IEnumerable<T> domainObjects)
        {
            DomainObjects = domainObjects;
        }
        public IEnumerable<T> DomainObjects { get; }
    }

    public class SaveAllCommandHandler<T> : IHandlerWrapper<SaveAllCommand<T>, Unit>
        where T : DomainObject
    {
        private readonly IDataService<T> _dataService;

        public SaveAllCommandHandler(IDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Response<Unit>> Handle(SaveAllCommand<T> request, CancellationToken cancellationToken)
        {
            await _dataService.SaveAllAsync(request.DomainObjects, cancellationToken);

            return Response.Ok();
        }
    }
}
