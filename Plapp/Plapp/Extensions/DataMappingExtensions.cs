using Plapp.Core;
using System.Collections.Generic;
using System.Linq;

namespace Plapp
{
    public static class DataMappingExtensions
    {
        public static ITopicViewModel ToViewModel(this Topic topic)
        {
            var topicViewModel =  new TopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                ImageUri = topic.ImageUri,
            };

            topicViewModel.AddDataSeries(topic.DataSeries.Select(d => d.ToViewModel(topicViewModel)));
            topicViewModel.AddNotes(topic.Notes.Select(n => n.ToViewModel(topicViewModel)));

            return topicViewModel;
        }

        public static INoteViewModel ToViewModel(this Note note, ITopicViewModel topicViewModel)
        {
            return new NoteViewModel
            {
                Id = note.Id,
                Header = note.Header,
                Text = note.Text,
                Date = note.Date,
                ImageUri = note.ImageUri,
                Topic = topicViewModel,
            };
        }

        public static IDataSeriesViewModel ToViewModel(this DataSeries dataSeries, ITopicViewModel topicViewModel)
        {
            var dataSeriesViewModel = new DataSeriesViewModel 
            { 
                Id = dataSeries.Id,
                TagId = dataSeries.TagId,
                Topic = topicViewModel,
            };

            dataSeriesViewModel.AddDataPoints(dataSeries.DataPoints.Select(d => d.ToViewModel(dataSeriesViewModel)));

            return dataSeriesViewModel;
        }

        public static IDataPointViewModel ToViewModel(this DataPoint dataPoint, IDataSeriesViewModel dataSeriesViewModel)
        {
            return new DataPointViewModel
            {
                Id = dataPoint.Id,
                Value = dataPoint.Value,
                Date = dataPoint.Date,
                DataSeries = dataSeriesViewModel,
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
                DataSeries = topicViewModel.DataEntries.Select(d => d.ToModel()).ToList(),
                Notes = topicViewModel.DiaryEntries.Select(n => n.ToModel()).ToList()
            };
        }

        public static Note ToModel(this INoteViewModel noteViewModel)
        {
            return new Note
            {
                Id = noteViewModel.Id,
                TopicId = noteViewModel.Topic.Id,
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
