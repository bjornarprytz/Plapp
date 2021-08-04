using AutoMapper;
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
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public class TopicViewModel : IOViewModel, ITopicViewModel
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _vmFactory;
        private readonly IMediator _mediator;

        public TopicViewModel(
            INavigator navigator,
            IViewModelFactory vmFactory,
            IMediator mediator
            )
        {
            _navigator = navigator;
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

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var response = await _mediator.Send(new GetAllDataSeriesQuery(Id));

            if (response.IsError)
                response.Throw();

            var freshDataSeries = response.Payload;

            DataSeries.Update(
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

            newDataSeries.Topic = this;
            newDataSeries.Tag = chosenTag;
            
            DataSeries.Add(newDataSeries);

            await _navigator.GoToAsync(newDataSeries);
        }
    }
}
