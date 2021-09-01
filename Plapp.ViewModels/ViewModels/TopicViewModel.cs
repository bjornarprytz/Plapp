﻿using System;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Interactive;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        private Task SaveTopicAsync()
        {
            _mapper.Map(this, _topic);

            return _topicService.SaveAsync(_topic);
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
                case Outcome.Error:
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
