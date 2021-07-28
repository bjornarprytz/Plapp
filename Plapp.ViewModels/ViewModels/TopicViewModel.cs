﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Interactive;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace Plapp.ViewModels
{
    public class TopicViewModel : IOViewModel, ITopicViewModel
    {
        private readonly ObservableCollection<IDataSeriesViewModel> _dataSeries;
        private readonly ICamera _camera;
        private readonly INavigator _navigator;
        private readonly ITagService _tagService;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ITopicService _topicService;
        private readonly IPrompter _prompter;
        private readonly ViewModelFactory<IDataSeriesViewModel> _dataSeriesFactory;
        private readonly ViewModelFactory<ITagViewModel> _tagFactory;
        private readonly IMediator _mediator;

        public TopicViewModel(
            ICamera camera,
            INavigator navigator,
            ITagService tagService,
            IDataSeriesService dataSeriesService,
            ITopicService topicService,
            IPrompter prompter,
            ViewModelFactory<IDataSeriesViewModel> dataSeriesFactory,
            ViewModelFactory<ITagViewModel> tagFactory,
            IMediator mediator
            )
        {
            _camera = camera;
            _navigator = navigator;
            _tagService = tagService;
            _dataSeriesService = dataSeriesService;
            _topicService = topicService;
            _prompter = prompter;
            _dataSeriesFactory = dataSeriesFactory;
            _tagFactory = tagFactory;
            _mediator = mediator;
            _dataSeries = new ObservableCollection<IDataSeriesViewModel>();
            DataSeries = new ReadOnlyObservableCollection<IDataSeriesViewModel>(_dataSeries);

            OpenCommand = new AsyncCommand(OpenTopic, allowsMultipleExecutions: false);
            AddImageCommand = new AsyncCommand(AddImage, allowsMultipleExecutions: false);
            AddDataSeriesCommand = new AsyncCommand(AddDataSeriesAsync, allowsMultipleExecutions: false);
        }

        public int Id { get; set; }
        public ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        public bool IsSavingTopic { get; private set; }
        
        [AlsoNotifyFor(nameof(ImageUri))]
        public bool LacksImage => string.IsNullOrEmpty(ImageUri);
        
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public IAsyncCommand OpenCommand { get; private set; }
        public IAsyncCommand AddImageCommand { get; private set; }
        public IAsyncCommand AddDataSeriesCommand { get; private set; }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var response = await _mediator.Send(new GetAllDataSeriesQuery(topicId: Id));

            if (response.Error)
                response.Throw();

            var freshDataSeries = response.Data;

            _dataSeries.Update(
                freshDataSeries,
                (v1, v2) => v1.Id == v2.Id);
        }

        protected override async Task AutoSaveDataAsync()
        {
            await base.AutoSaveDataAsync();

            await _mediator.Send(new SaveTopicCommand(this));
        }

        private async Task OpenTopic()
        {
            await _navigator.GoToAsync<ITopicViewModel>(this);
        }

        private async Task AddImage()
        {
            using var photoStream = await _camera.TakePhotoAsync();

            if (photoStream == null)
            {
                return;
            }

            ImageUri = await FileSystem.AppDataDirectory.SaveAsync($"{Guid.NewGuid()}.jpg", photoStream);
        }

        private async Task AddDataSeriesAsync()
        {
            var chooseResult = await _mediator.Send(new PickTagAction());

            if (chooseResult.Error)
                chooseResult.Throw();

            var chosenTag = chooseResult.Data;

            if (chosenTag == null) return;

            var newDataSeries = _dataSeriesFactory();

            newDataSeries.Topic = this;
            newDataSeries.Tag = chosenTag;

            _dataSeries.Add(newDataSeries);

            var saveResult = await _mediator.Send(new SaveDataSeriesCommand(newDataSeries));

            if (saveResult.Error)
                saveResult.Throw();

            await _navigator.GoToAsync(newDataSeries);
        }
    }
}
