using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plapp.ViewModels
{
    public static class DataMappingExtensions
    {
        public static ITopicViewModel ToViewModel(this Topic topic, IServiceProvider sp)
        {
            var topicViewModel =  new TopicViewModel(sp)
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                ImageUri = topic.ImageUri,
            };
            if (topic.DataSeries != null && topic.DataSeries.Any())
                topicViewModel.AddDataSeries(topic.DataSeries.Select(d => d.ToViewModel(topicViewModel, sp)));

            return topicViewModel;
        }

        public static IDataSeriesViewModel ToViewModel(this DataSeries dataSeries, ITopicViewModel topicViewModel, IServiceProvider sp)
        {
            if (dataSeries.TopicId != topicViewModel.Id)
                throw new ArgumentException("Topic Id mismatch between new parent");

            var dataSeriesViewModel = new DataSeriesViewModel(sp)
            { 
                Id = dataSeries.Id,
                TagId = dataSeries.TagId,
                Topic = topicViewModel,
            };

            if (dataSeries.DataPoints != null && dataSeries.DataPoints.Any())
                dataSeriesViewModel.AddDataPoints(dataSeries.DataPoints.Select(d => d.ToViewModel(dataSeriesViewModel, sp)));

            return dataSeriesViewModel;
        }

        public static IDataPointViewModel ToViewModel(this DataPoint dataPoint, IDataSeriesViewModel dataSeriesViewModel, IServiceProvider sp)
        {
            return new DataPointViewModel(sp)
            {
                Id = dataPoint.Id,
                Value = dataPoint.Value,
                Date = dataPoint.Date,
                DataSeries = dataSeriesViewModel,
            };
        }

        public static ITagViewModel ToViewModel(this Tag tag, IServiceProvider sp)
        {
            return new TagViewModel(sp)
            {
                Id = tag.Id,
                Unit = tag.Unit,
                Color = tag.Color,
                DataType = tag.DataType,
                Icon = tag.Icon,
            };
        }

        public static Topic ToModel(this ITopicViewModel topicViewModel)
        {
            return new Topic
            {
                Id = topicViewModel.Id,
                Title = topicViewModel.Title,
                Description = topicViewModel.Description,
                ImageUri = topicViewModel.ImageUri,
                DataSeries = topicViewModel.DataEntries?.Select(d => d.ToModel()).ToList(),
            };
        }

        public static DataSeries ToModel(this IDataSeriesViewModel dataSeriesViewModel)
        {
            var dataSeries = new DataSeries
            {
                Id = dataSeriesViewModel.Id,
                TopicId = dataSeriesViewModel.Topic.Id,
                TagId = dataSeriesViewModel.Tag.Id,
                DataPoints = new List<DataPoint>(),
            };

            foreach(var dataSerie in dataSeriesViewModel.GetDataPoints())
            {
                dataSeries.DataPoints.Add(dataSerie.ToModel());
            }

            return dataSeries;
        }

        public static DataPoint ToModel(this IDataPointViewModel dataPoinViewModel)
        {
            return new DataPoint
            {
                Id = dataPoinViewModel.Id,
                DataSeriesId = dataPoinViewModel.DataSeries.Id,
                Value = dataPoinViewModel.Value,
                Date = dataPoinViewModel.Date,
            };
        }

        public static Tag ToModel(this ITagViewModel tagViewModel)
        {
            return new Tag
            {
                Id = tagViewModel.Id,
                Unit = tagViewModel.Unit,
                Color = tagViewModel.Color,
                DataType = tagViewModel.DataType,
                Icon = tagViewModel.Icon,
            };
        }
    }
}
