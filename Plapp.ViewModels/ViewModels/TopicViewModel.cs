using System;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Interactive;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    
    [QueryProperty(nameof(Id), nameof(Id))]
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly SourceCache<IDataSeriesViewModel, int> _dataSeriesMutable = new (topic => topic.Id);
        private readonly ReadOnlyObservableCollection<IDataSeriesViewModel> _dataSeries;
        private readonly IMediator _mediator;

        public TopicViewModel(IMediator mediator)
        {
            _mediator = mediator;

            OpenCommand = new AsyncCommand(OpenTopic, allowsMultipleExecutions: false);
            AddImageCommand = new AsyncCommand(AddImage, allowsMultipleExecutions: false);
            AddDataSeriesCommand = new AsyncCommand(AddDataSeriesAsync, allowsMultipleExecutions: false);

            _dataSeriesMutable
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _dataSeries)
                .DisposeMany()
                .Subscribe();
        }

        public int Id { get; set; }
        public ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries => _dataSeries;
        
        [Reactive] public bool LacksImage => ImageUri.IsMissing();
        [Reactive] public string ImageUri { get; set; }
        [Reactive] public string Title { get; set; }
        [Reactive] public string Description { get; set; }

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

            _dataSeriesMutable.Edit(innerList =>
            {
                innerList.Clear();
                innerList.AddOrUpdate(freshDataSeries);
            });
        }

        private Task SaveTopicAsync()
        {
            return _mediator.Send(new SaveTopicCommand(this));
        }

        private async Task OpenTopic()
        {
            await Shell.Current.GoToAsync($"topic?Id={Id}");
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
            await Shell.Current.GoToAsync($"data-series");
        }
    }
}
