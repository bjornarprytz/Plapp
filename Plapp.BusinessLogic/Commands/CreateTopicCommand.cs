using AutoMapper;
using MediatR;
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
        private readonly ITopicService _topicService;
        private readonly ViewModelFactory<ITopicViewModel> _viewModelFactory;
        private readonly IMapper _mapper;

        public CreateTopicCommandHandler(
            IMediator mediator,
            ITopicService topicService,
            ViewModelFactory<ITopicViewModel> viewModelFactory,
            IMapper mapper
            )
        {
            _mediator = mediator;
            _topicService = topicService;
            _viewModelFactory = viewModelFactory;
            _mapper = mapper;
        }

        public async Task<Response<ITopicViewModel>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var viewModel = _viewModelFactory();

            await _mediator.Send(new SaveTopicCommand(viewModel), cancellationToken);

            return Response.Ok(viewModel);
        }
    }
}
