using MediatR;
using Plapp.BusinessLogic;
using Plapp.Core;
using System.Threading.Tasks;
using Plapp.BusinessLogic.Commands;

namespace Plapp.ViewModels
{
    public class EditTagViewModel : EditViewModel<ITagViewModel>
    {
        private readonly IMediator _mediator;
        
        public EditTagViewModel(
            IMediator mediator,
            IViewModelFactory vmFactory,
            ICompositeValidator<ITagViewModel> validators)
        : base(vmFactory, validators)
        {
            _mediator = mediator;
        }
        

        protected override Task OnConfirm()
        {
            return _mediator.Send(new SaveTagCommand(ToCreate));
        }
    }
}
