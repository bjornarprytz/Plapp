﻿using MediatR;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class CreateTopicCommand : IRequestWrapper<ITopicViewModel>
    {
    }

    public class CreateTopicCommandHandler : IHandlerWrapper<CreateTopicCommand, ITopicViewModel>
    {
        private readonly IMediator _mediator;
        private readonly ViewModelFactory<ITopicViewModel> _viewModelFactory;

        public CreateTopicCommandHandler(
            IMediator mediator,
            ViewModelFactory<ITopicViewModel> viewModelFactory
            )
        {
            _mediator = mediator;
            _viewModelFactory = viewModelFactory;
        }

        public async Task<Response<ITopicViewModel>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var viewModel = _viewModelFactory();

            await _mediator.Send(new SaveTopicCommand(viewModel), cancellationToken);

            return Response.Ok(viewModel);
        }
    }
}
