using MediatR;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class SaveCommand<T> : IRequestWrapper<T>
        where T : DomainObject
    {
        internal SaveCommand(T domainObject)
        {
            DomainObject = domainObject;
        }

        public T DomainObject { get; }
    }

    public class SaveCommandHandler<T> : IHandlerWrapper<SaveCommand<T>, T>
        where T : DomainObject
    {
        private readonly IDataService<T> _dataService;

        internal SaveCommandHandler(IDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Response<T>> Handle(SaveCommand<T> request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _dataService.SaveAsync(request.DomainObject, cancellationToken));
        }
    }

}
