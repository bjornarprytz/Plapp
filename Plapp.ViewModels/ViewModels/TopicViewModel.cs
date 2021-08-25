using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Interactive;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    
    [QueryProperty(nameof(TopicViewModel.Id), nameof(TopicViewModel.Id))]
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly IViewModelFactory _vmFactory;
        private readonly IMediator _mediator;

        public TopicViewModel(
            IViewModelFactory vmFactory,
            IMediator mediator
            )
        {
            _vmFactory = vmFactory;
            _mediator = mediator;
            
            DataSeries = new ObservableCollection<IDataSeriesViewModel>();

            OpenCommand = new AsyncCommand(OpenTopic, allowsMultipleExecutions: false);
            AddImageCommand = new AsyncCommand(AddImage, allowsMultipleExecutions: false);
            AddDataSeriesCommand = new AsyncCommand(AddDataSeriesAsync, allowsMultipleExecutions: false);
        }

        public int Id { get; set; }
        public ObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        public bool IsSavingTopic { get; private set; }
        
        [AlsoNotifyFor(nameof(ImageUri))]
        public bool LacksImage => string.IsNullOrEmpty(ImageUri);
        
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public IAsyncCommand OpenCommand { get; private set; }
        public IAsyncCommand AddImageCommand { get; private set; }
        public IAsyncCommand AddDataSeriesCommand { get; private set; }

        public override Task AppearingAsync()
        {
            return LoadDataSeriesAsync();
        }

        public override Task DisappearingAsync()
        {
            return SaveTopicAsync();
        }

        private async Task LoadDataSeriesAsync()
        {
            var response = await _mediator.Send(new GetAllDataSeriesQuery(Id));

            if (response.IsError)
                response.Throw();

            var freshDataSeries = response.Payload;

            DataSeries.Update(
                freshDataSeries,
                (v1, v2) => v1.Id == v2.Id);
        }

        private Task SaveTopicAsync()
        {
            return _mediator.Send(new SaveTopicCommand(this));
        }

        private async Task OpenTopic()
        {
            await _mediator.Send(new NavigateAction("topic")); // Specify parameters for this
        }

        private async Task AddImage()
        {
            var response = await _mediator.Send(new TakePhotoAction());

            switch (response.Outcome)
            {
                case Outcome.Cancel: return;
                case Outcome.Ok:
                    ImageUri = response.Payload;
                    break;
                default:
                    response.Throw();
                    break;
            }
        }

        private async Task AddDataSeriesAsync()
        {
            var tagResponse = await _mediator.Send(new PickTagAction());
            
            if (tagResponse.IsCancelled)
                return;

            if (tagResponse.IsError)
                tagResponse.Throw();

            var chosenTag = tagResponse.Payload;
            
            var newDataSeries = _vmFactory.Create<IDataSeriesViewModel>();

            newDataSeries.Tag = chosenTag;
            
            DataSeries.Add(newDataSeries);

            await _mediator.Send(new NavigateAction("data-series"));  // TODO: Specify parameters for data series
        }
    }
}
