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
                Title = dataSeries.Title,
                Tag = dataSeries.Tag.ToViewModel(sp),
                Topic = topicViewModel,
            };

            if (dataSeries.DataPoints != null && dataSeries.DataPoints.Any())
                dataSeriesViewModel.AddDataPoints(dataSeries.DataPoints.Select(d => d.ToViewModel(sp)));

            return dataSeriesViewModel;
        }

        public static IDataPointViewModel ToViewModel(this DataPoint dataPoint, IServiceProvider sp)
        {
            return new DataPointViewModel(sp)
            {
                Id = dataPoint.Id,
                Value = dataPoint.Value,
                Date = dataPoint.Date,
            };
        }

        public static ITagViewModel ToViewModel(this Tag tag, IServiceProvider sp)
        {
            return new TagViewModel(sp)
            {
                Id = tag.Id,
                Key = tag.Key,
                Unit = tag.Unit,
                Color = tag.Color,
                DataType = tag.DataType,
                Icon = tag.Icon,
            };
        }

        public static Topic ToModel(this ITopicViewModel topicViewModel)
        {
            var topic = new Topic
            {
                Id = topicViewModel.Id,
                Title = topicViewModel.Title,
                Description = topicViewModel.Description,
                ImageUri = topicViewModel.ImageUri,
                DataSeries = new List<DataSeries>(),
            };

            foreach(var dataSeries in topicViewModel.DataSeries)
            {
                topic.DataSeries.Add(dataSeries.ToModel());
            }

            return topic;
        }

        public static DataSeries ToModel(this IDataSeriesViewModel dataSeriesViewModel)
        {
            var dataSeries = new DataSeries
            {
                Id = dataSeriesViewModel.Id,
                Title = dataSeriesViewModel.Title,
                TopicId = dataSeriesViewModel.Topic?.Id ?? default,
                TagId = dataSeriesViewModel.Tag?.Id ?? default,
                DataPoints = new List<DataPoint>(),
            };

            foreach(var dataSerie in dataSeriesViewModel.DataPoints)
            {
                dataSeries.DataPoints.Add(dataSerie.ToModel(dataSerie.Id));
            }

            return dataSeries;
        }

        public static DataPoint ToModel(this IDataPointViewModel dataPoinViewModel, int dataSeriesId)
        {
            return new DataPoint
            {
                Id = dataPoinViewModel.Id,
                DataSeriesId = dataSeriesId,
                Value = dataPoinViewModel.Value,
                Date = dataPoinViewModel.Date,
            };
        }

        public static Tag ToModel(this ITagViewModel tagViewModel)
        {
            return new Tag
            {
                Id = tagViewModel.Id,
                Key = tagViewModel.Key,
                Unit = tagViewModel.Unit,
                Color = tagViewModel.Color,
                DataType = tagViewModel.DataType,
                Icon = tagViewModel.Icon,
            };
        }
    }
}
