using System;
using System.Collections.Generic;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Interactive;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class DataSeriesViewModel : BaseViewModel, IDataSeriesViewModel
    {
        private readonly SourceCache<IDataPointViewModel, int> _dataPointsMutable = new (dataPoint => dataPoint.Id);
        private readonly ReadOnlyObservableCollection<IDataPointViewModel> _dataPoints;
        private readonly IMediator _mediator;
        private readonly ITagService _tagService;

        public DataSeriesViewModel(IMediator mediator,
            ITagService tagService)
        {
            _mediator = mediator;
            _tagService = tagService;

            AddDataPointCommand = ReactiveCommand.CreateFromTask(AddDataPointsAsync);
            OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
            PickTagCommand = ReactiveCommand.CreateFromTask(PickTagAsync);

            _dataPointsMutable
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _dataPoints)
                .Subscribe();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        
        public int TagId { get; set; }
        public ITagViewModel Tag { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints => _dataPoints;

        public ICommand AddDataPointCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand PickTagCommand { get; private set; }

        public override Task AppearingAsync()
        {
            return LoadDataPointsAsync();
        }

        public override Task DisappearingAsync()
        {
            return SaveDataSeriesAsync();
        }

        private async Task LoadDataPointsAsync()
        {
            var dataPointsResponse = await _mediator.Send(new GetAllDataPointsQuery(Id));

            if (dataPointsResponse.IsError)
                dataPointsResponse.Throw();

            var dataPoints = dataPointsResponse.Payload;

            _dataPointsMutable.Edit(innerList =>
            {
                innerList.Clear();
                innerList.AddOrUpdate(dataPoints);
            });
        }

        private async Task SaveDataSeriesAsync()
        {
            await _mediator.Send(new SaveDataSeriesCommand(this));
        }

        private async Task AddDataPointsAsync()
        {
            if (Tag == default)
            {
                var tagResponse = await _mediator.Send(new PickTagAction());
            
                if (tagResponse.IsCancelled)
                    return;

                if (tagResponse.IsError)
                    tagResponse.Throw();

                Tag = tagResponse.Payload;
            }
            
            var dataPointsResponse = await _mediator.Send(new CreateDataPointsAction(Tag));

            if (dataPointsResponse.IsCancelled)
            {
                return;
            }

            if (dataPointsResponse.IsError)
                dataPointsResponse.Throw();

            var dataPoints = dataPointsResponse.Payload;

            _dataPointsMutable.Edit(innerList =>
            {
                innerList.Clear();
                innerList.AddOrUpdate(dataPoints);
            });
        }

        private async Task PickTagAsync()
        {
            var chooseResult = await _mediator.Send(new PickTagAction());

            if (chooseResult.IsCancelled)
                return;

            if (chooseResult.IsError)
                chooseResult.Throw();

            var chosenTag = chooseResult.Payload;
            
            Tag = chosenTag;
        }

        private async Task OpenAsync()
        {
            await _mediator.Send(new NavigateAction($"data-series?Id={Id}"));
        }
    }
}
