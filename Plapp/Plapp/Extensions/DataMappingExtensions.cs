using Plapp.Core;
using System.Collections.Generic;
using System.Linq;

namespace Plapp
{
    public static class DataMappingExtensions
    {
        public static ITopicViewModel ToViewModel(this Topic topic)
        {
            return new TopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                ImageUri = topic.ImageUri,
            };
        }

        public static INoteViewModel ToViewModel(this Note note)
        {
            return new NoteViewModel
            {
                Id = note.Id,
                Header = note.Header,
                Text = note.Text,
                Date = note.Date,
                ImageUri = note.ImageUri,               
            };
        }

        public static IDataSeriesViewModel ToViewModel(this DataSeries dataSeries)
        {
            var dataSeriesViewModel = new DataSeriesViewModel 
            { 
                Id = dataSeries.Id,
                Tag = dataSeries.Tag.ToViewModel(),
            };

            dataSeriesViewModel.AddDataPoints(
                dataSeries.DataPoints.Select(ToViewModel)
                );

            return dataSeriesViewModel;
        }

        public static IDataPointViewModel ToViewModel(this DataPoint dataPoint)
        {
            return new DataPointViewModel
            {
                Id = dataPoint.Id,
                Data = dataPoint.Value,
                Date = dataPoint.Date,
            };
        }

        public static ITagViewModel ToViewModel(this Tag tag)
        {
            return new TagViewModel
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
            };
        }

        public static Note ToModel(this INoteViewModel noteViewModel)
        {
            return new Note
            {
                Id = noteViewModel.Id,
                Header = noteViewModel.Header,
                Text = noteViewModel.Text,
                Date = noteViewModel.Date,
                ImageUri = noteViewModel.ImageUri,
            };
        }

        public static DataSeries ToModel(this IDataSeriesViewModel dataSeriesViewModel)
        {
            var dataSeries = new DataSeries
            {
                Id = dataSeriesViewModel.Id,
                Tag = dataSeriesViewModel.Tag.ToModel(),
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
                Value = dataPoinViewModel.Data,
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
