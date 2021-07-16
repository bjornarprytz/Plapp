using MediatR;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class DeleteCommand<T> : IRequestWrapper<Unit>
        where T : DomainObject
    {
        internal DeleteCommand(int id)
        {
            Id = id;
        }

        internal DeleteCommand(T domainObject)
        {
            Id = domainObject.Id;
        }

        public int Id { get; }
    }

    public class DeleteCommandHandler<T> : IHandlerWrapper<DeleteCommand<T>, Unit>
        where T : DomainObject
    {
        private readonly IDataService<T> _dataService;

        public DeleteCommandHandler(IDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Response<Unit>> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            var result = await _dataService.DeleteAsync(request.Id, cancellationToken);

            if (!result)
            {
                return Response.Fail($"Failed to delete object with Id {request.Id} and type {typeof(T)}");
            }

            return Response.Ok();
        }
    }
}
