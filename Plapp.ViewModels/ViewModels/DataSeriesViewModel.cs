using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Interactive;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : IOViewModel, IDataSeriesViewModel
    {
        private readonly INavigator _navigator;
        private readonly IPrompter _prompter;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ViewModelFactory<IDataPointViewModel> _dataPointFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DataSeriesViewModel(
            INavigator navigator,
            IPrompter prompter,
            IDataSeriesService dataSeriesService,
            ViewModelFactory<IDataPointViewModel> dataPointFactory,
            IMapper mapper,
            IMediator mediator
            )
        {
            _navigator = navigator;
            _prompter = prompter;
            _dataSeriesService = dataSeriesService;
            _dataPointFactory = dataPointFactory;
            _mapper = mapper;
            _mediator = mediator;

            DataPoints = new ObservableCollection<IDataPointViewModel>();

            AddDataPointCommand = new AsyncCommand(AddDataPointsAsync, allowsMultipleExecutions: false);
            OpenCommand = new AsyncCommand(OpenAsync, allowsMultipleExecutions: false);
            PickTagCommand = new AsyncCommand(PickTagAsync, allowsMultipleExecutions: false);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ObservableCollection<IDataPointViewModel> DataPoints { get; }

        public IAsyncCommand AddDataPointCommand { get; private set; }
        public IAsyncCommand OpenCommand { get; private set; }
        public IAsyncCommand PickTagCommand { get; private set; }

        protected override async Task AutoLoadDataAsync()
        {
            var dataPointsResponse = await _mediator.Send(new GetAllDataPointsQuery(Id));

            if (dataPointsResponse.Error)
                dataPointsResponse.Throw();

            var dataPoints = dataPointsResponse.Data;

            DataPoints.Update(
                dataPoints,
                (v1, v2) => v1.Id == v2.Id);
        }

        protected override async Task AutoSaveDataAsync()
        {
            await _dataSeriesService.SaveAsync(_mapper.Map<DataSeries>(this));
        }

        private async Task AddDataPointsAsync()
        {
            var dataPoints = await _prompter.CreateMultipleAsync(
                    () => _dataPointFactory() // TODO: Make different DataPoints depending on Tag.DataType
                );

            if (dataPoints == default || !dataPoints.Any())
            {
                return;
            }

            DataPoints.AddRange(dataPoints);

            OnPropertyChanged(nameof(DataPoints));
        }

        private async Task PickTagAsync()
        {
            var chooseResult = await _mediator.Send(new PickTagAction());

            if (chooseResult.Cancelled)
                return;

            if (chooseResult.Error)
                chooseResult.Throw();

            var chosenTag = chooseResult.Data;
            
            Tag = chosenTag;
        }

        private async Task OpenAsync()
        {
            await _navigator.GoToAsync<IDataSeriesViewModel>(this);
        }
    }
}
