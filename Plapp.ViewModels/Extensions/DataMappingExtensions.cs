using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plapp.ViewModels
{
    internal static class DataMappingExtensions
    {
        internal static TViewModel ToViewModel<TDomainObject, TViewModel>(this TDomainObject domainObject, Func<TViewModel> createVM)
            where TDomainObject : DomainObject
            where TViewModel : IViewModel, IHydrate<TDomainObject>
        {
            var vm = createVM();

            vm.Hydrate(domainObject);

            return vm;
        }

        internal static Topic ToModel(this ITopicViewModel topicViewModel)
        {
            var topic = new Topic
            {
                Id = topicViewModel.Id,
                Title = topicViewModel.Title,
                Description = topicViewModel.Description,
                ImageUri = topicViewModel.ImageUri,
                DataSeries = topicViewModel.DataSeries?.Select(ds => ds.ToModel()).ToList() ?? new List<DataSeries>(),
            };

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
                DataPoints = dataSeriesViewModel.DataPoints?.Select(dp => dp.ToModel(dataSeriesViewModel.Id)).ToList() ?? new List<DataPoint>(),
            };

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
