using System;
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
using AutoMapper;
using DynamicData;
using Plapp.ViewModels.Infrastructure;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    
    [QueryProperty(nameof(Id), nameof(Id))]
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly SourceCache<IDataSeriesViewModel, int> _dataSeriesMutable = new (topic => topic.Id);
        private readonly ReadOnlyObservableCollection<IDataSeriesViewModel> _dataSeries;

        private Topic _topic;
        
        private readonly IMediator _mediator;
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public TopicViewModel(
            IMediator mediator, 
            ITopicService topicService,
            IMapper mapper)
        {
            _mediator = mediator;
            _topicService = topicService;
            _mapper = mapper;

            DeleteCommand = new PlappCommand(DeleteAsync);
            OpenCommand = new PlappCommand(OpenAsync);
            AddImageCommand = new PlappCommand(AddImageAsync);
            AddDataSeriesCommand = new PlappCommand(AddDataSeriesAsync);

            _dataSeriesMutable
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _dataSeries)
                .DisposeMany()
                .Subscribe();
        }

        public int Id { get; set; }
        public ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries => _dataSeries;
        
        [Reactive] public string ImageUri { get; set; }
        [Reactive] public string Title { get; set; }
        [Reactive] public string Description { get; set; }
        

        public ICommand DeleteCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand AddDataSeriesCommand { get; }

        public override Task AppearingAsync()
        {
            return LoadDataSeriesAsync();
        }

        public override Task DisappearingAsync()
        {
            return SaveAsync();
        }
        
        private async Task LoadDataSeriesAsync()
        {
            _topic = await _topicService.FetchAsync(Id);
            _mapper.Map(_topic, this);
            
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

        private async Task DeleteAsync()
        {
            var result = await _mediator.Send(new DeleteTopicCommand(this));

            if (!result.IsValid)
                return;

            await Shell.Current.GoToAsync("..");
        }

        private Task SaveAsync()
        {
            return _mediator.Send(new SaveTopicCommand(this));
        }

        private async Task OpenAsync()
        {
            await Shell.Current.GoToAsync($"topic?Id={Id}");
        }

        private async Task AddImageAsync()
        {
            var response = await _mediator.Send(new TakePhotoAction());

            switch (response.Outcome)
            {
                case Outcome.Cancel: return;
                case Outcome.Ok:
                    ImageUri = response.Payload;
                    break;
                case Outcome.Error:
                default:
                    response.Throw();
                    break;
            }
        }

        private Task AddDataSeriesAsync()
        {
            return Shell.Current.GoToAsync($"data-series");
        }
    }
}
