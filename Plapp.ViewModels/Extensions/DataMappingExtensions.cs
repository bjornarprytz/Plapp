using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plapp.ViewModels
{
    internal static class DataMappingExtensions
    {
        internal static TopicViewModel ToViewModel(this Topic topic, IServiceProvider sp)
        {
            var topicViewModel = new TopicViewModel(sp);

            topicViewModel.Hydrate(topic);

            return topicViewModel;
        }

        internal static DataSeriesViewModel ToViewModel(this DataSeries dataSeries, IServiceProvider sp)
        {
            var dataSeriesViewModel = new DataSeriesViewModel(sp);

            dataSeriesViewModel.Hydrate(dataSeries);

            return dataSeriesViewModel;
        }

        internal static DataPointViewModel ToViewModel(this DataPoint dataPoint, IServiceProvider sp)
        {
            var dataPointViewModel = new DataPointViewModel(sp);

            dataPointViewModel.Hydrate(dataPoint);

            return dataPointViewModel;
        }

        internal static ITagViewModel ToViewModel(this Tag tag, IServiceProvider sp)
        {
            var tagViewModel = new TagViewModel(sp);

            tagViewModel.Hydrate(tag);

            return tagViewModel;
        }

        internal static Topic ToModel(this ITopicViewModel topicViewModel)
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

        internal static DataSeries ToModel(this IDataSeriesViewModel dataSeriesViewModel)
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

        internal static DataPoint ToModel(this IDataPointViewModel dataPoinViewModel, int dataSeriesId)
        {
            return new DataPoint
            {
                Id = dataPoinViewModel.Id,
                DataSeriesId = dataSeriesId,
                Value = dataPoinViewModel.Value,
                Date = dataPoinViewModel.Date,
            };
        }

        internal static Tag ToModel(this ITagViewModel tagViewModel)
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
