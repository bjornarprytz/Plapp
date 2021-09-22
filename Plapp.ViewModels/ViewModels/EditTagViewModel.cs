using MediatR;
using Plapp.BusinessLogic;
using Plapp.Core;
using System.Threading.Tasks;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    [QueryProperty(nameof(TagId), nameof(TagId))]
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
        
        
        public int TagId { get; set; } // TODO: Can this be private?
        
        public override async Task AppearingAsync()
        {
            var tagResult = await _mediator.Send(new GetTagQuery(TagId));

            if (tagResult.IsValid)
                ToCreate = tagResult.Payload;
        }

        protected override Task OnConfirm()
        {
            return _mediator.Send(new SaveTagCommand(ToCreate));
        }
    }
}
