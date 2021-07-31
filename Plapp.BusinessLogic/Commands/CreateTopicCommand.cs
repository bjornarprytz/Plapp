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
        private readonly ViewModelFactory<ITopicViewModel> _viewModelFactory;

        public CreateTopicCommandHandler(
            ViewModelFactory<ITopicViewModel> viewModelFactory
            )
        {
            _viewModelFactory = viewModelFactory;
        }

        public Task<Response<ITopicViewModel>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var viewModel = _viewModelFactory();

            return Task.FromResult(Response.Ok(viewModel));
        }
    }
}
