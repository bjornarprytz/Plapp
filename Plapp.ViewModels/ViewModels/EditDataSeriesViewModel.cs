using System.Threading.Tasks;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    [QueryProperty(nameof(DataSeriesId), nameof(DataSeriesId))]
    public class EditDataSeriesViewModel : EditViewModel<IDataSeriesViewModel>
    {
        private readonly IMediator _mediator;

        public EditDataSeriesViewModel(IMediator mediator, IViewModelFactory viewModelFactory, ICompositeValidator<IDataSeriesViewModel> validators) : base(viewModelFactory, validators)
        {
            _mediator = mediator;
        }

        public int DataSeriesId { get; set; } // TODO: Can this be private?
        
        public override async Task AppearingAsync()
        {
            var dataSeriesResult = await _mediator.Send(new GetDataSeriesQuery(DataSeriesId));

            if (dataSeriesResult.IsValid)
                ToCreate = dataSeriesResult.Payload;
        }

        protected override Task OnConfirm()
        {
             return _mediator.Send(new SaveDataSeriesCommand(ToCreate));
        }
    }
}