using AutoMapper;
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
        private readonly ITopicService _topicService;
        private readonly ViewModelFactory<ITopicViewModel> _viewModelFactory;
        private readonly IMapper _mapper;

        public CreateTopicCommandHandler(
            ITopicService topicService,
            ViewModelFactory<ITopicViewModel> viewModelFactory,
            IMapper mapper
            )
        {
            _topicService = topicService;
            _viewModelFactory = viewModelFactory;
            _mapper = mapper;
        }

        public async Task<Response<ITopicViewModel>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var viewModel = _viewModelFactory();

            await _topicService.SaveAsync(_mapper.Map<Topic>(viewModel), cancellationToken);

            return Response.Ok(viewModel);
        }
    }
}
