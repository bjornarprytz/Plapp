﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
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
        private readonly IMediator _mediator;

        public DataSeriesViewModel(
            INavigator navigator,
            IMediator mediator
            )
        {
            _navigator = navigator;
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
            await _mediator.Send(new SaveDataSeriesCommand(this));
        }

        private async Task AddDataPointsAsync()
        {
            var dataPointsResponse = await _mediator.Send(new CreateDataPointsAction());

            if (dataPointsResponse.Cancelled)
            {
                return;
            }

            if (dataPointsResponse.Error)
                dataPointsResponse.Throw();

            var dataPoints = dataPointsResponse.Data;

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
