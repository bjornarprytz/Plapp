using System.Threading.Tasks;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    [QueryProperty(nameof(DataSeriesId), nameof(DataSeriesId))]
    public class CreateDataPointsViewModel : CreateViewModel<IDataSeriesViewModel>
    {
        private readonly IMediator _mediator;

        public CreateDataPointsViewModel(IMediator mediator, IViewModelFactory viewModelFactory, ICompositeValidator<IDataSeriesViewModel> validators) : base(viewModelFactory, validators)
        {
            _mediator = mediator;
        }

        public int DataSeriesId { get; set; }
        
        public override Task AppearingAsync()
        {
            return base.AppearingAsync();
        }

        protected override async Task SaveViewModelAsync()
        {
            await _mediator.Send(new SaveDataSeriesCommand(ToCreate));

            await Shell.Current.GoToAsync("..");
        }
    }
}