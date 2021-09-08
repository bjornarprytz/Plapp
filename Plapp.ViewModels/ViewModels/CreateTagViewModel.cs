using MediatR;
using Plapp.BusinessLogic;
using Plapp.Core;
using System.Threading.Tasks;
using Plapp.BusinessLogic.Commands;

namespace Plapp.ViewModels
{
    public class CreateTagViewModel : CreateViewModel<ITagViewModel>
    {
        private readonly IMediator _mediator;
        
        public CreateTagViewModel(
            IMediator mediator,
            IViewModelFactory vmFactory,
            ICompositeValidator<ITagViewModel> validators)
        : base(vmFactory, validators)
        {
            _mediator = mediator;
        }

        protected override Task SaveViewModelAsync()
        {
            return _mediator.Send(new SaveTagCommand(ToCreate));
        }
    }
}
