using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.Persist.Tests
{
    [TestClass]
    public class DataSeriesServiceTests : BaseDataServiceTests<DataSeriesService, DataSeries>
    {
        [TestMethod]
        public async Task FetchAllAsync_WithTagId()
        {
            var tag1 = new Tag { Id = 1 };
            var tag2 = new Tag { Id = 2 };

            SeedDbWith(tag1, tag2);

            var item1 = new DataSeries { Id = 1, TagId = 1 };
            var item2 = new DataSeries { Id = 2, TagId = 1 };
            var item3 = new DataSeries { Id = 3, TagId = 2 };

            SeedDbWith(item1, item2, item3);

            var items = await dataService.FetchAllAsync(tagId: 1);

            Assert.IsTrue(items.Count() == 2);
            Assert.IsTrue(items.Any(item => item.Id == 1));
            Assert.IsTrue(items.Any(item => item.Id == 2));
        }

        [TestMethod]
        public async Task FetchAllAsync_WithTopicId()
        {
            var topic1 = new Topic { Id = 1 };
            var topic2 = new Topic { Id = 2 };

            SeedDbWith(topic1, topic2);

            var item1 = new DataSeries { Id = 1, TopicId = 1, Tag = new Tag() };
            var item2 = new DataSeries { Id = 2, TopicId = 1, Tag = new Tag() };
            var item3 = new DataSeries { Id = 3, TopicId = 2, Tag = new Tag() };

            SeedDbWith(item1, item2, item3);

            var items = await dataService.FetchAllAsync(topicId: 1);

            Assert.IsTrue(items.Count() == 2);
            Assert.IsTrue(items.Any(item => item.Id == 1));
            Assert.IsTrue(items.Any(item => item.Id == 2));
        }

        [TestMethod]
        public async Task FetchAllAsync_WithTopicId_AndTagId()
        {
            var topic1 = new Topic { Id = 1 };
            var topic2 = new Topic { Id = 2 };
            var tag1 = new Tag { Id = 1 };
            var tag2 = new Tag { Id = 2 };

            SeedDbWith(topic1, topic2);
            SeedDbWith(tag1, tag2);

            var item1 = new DataSeries { Id = 1, TopicId = 1, TagId = 1 };
            var item2 = new DataSeries { Id = 2, TopicId = 1, TagId = 2 };
            var item3 = new DataSeries { Id = 3, TopicId = 2 };

            SeedDbWith(item1, item2, item3);

            var items = await dataService.FetchAllAsync(topicId: 1, tagId: 1);

            Assert.IsTrue(items.Count() == 1);
            Assert.IsTrue(items.Any(item => item.Id == 1));
        }

        [TestMethod]
        public async Task SaveAsync_IncludesDataPoints()
        {
            var item1 = new DataSeries { 
                DataPoints = new Collection<DataPoint>() 
                { 
                    new DataPoint { Value = 1 },
                    new DataPoint { Value = 2 },
                } 
            };

            await dataService.SaveAsync(item1);

            using var context = new PlappDbContext(options);

            Assert.IsTrue(context.Set<DataPoint>().Count() == 2);
            Assert.IsFalse(context.Set<DataPoint>().Any(item => item.Id == 0));
        }

        [TestMethod]
        public async Task SaveAsync_IncludesTag()
        {
            var tag1 = new Tag { Id = 1 };

            SeedDbWith(tag1);

            var mutatedTag = new Tag { Id = 1, Color = "other_color" };

            var item1 = new DataSeries
            {
                TagId = 1,
                Tag = mutatedTag,
            };

            await dataService.SaveAsync(item1);

            using var context = new PlappDbContext(options);

            Assert.IsTrue(context.Set<Tag>().Count() == 1);

            var resultTag = context.Set<Tag>().FirstOrDefault(t => t.Id == 1);

            Assert.IsNotNull(resultTag);
            resultTag.Should().BeEquivalentTo(mutatedTag);
        }

        // TODO:
        // Check that DataSeries includes DataPoints and Tag

        [TestMethod]
        public async Task FetchAsync_IncludesTag()
        {
            var tag1 = new Tag { Id = 1, Color = "color" };

            SeedDbWith(tag1);

            var item1 = new DataSeries
            {
                Id = 1,
                TagId = 1,
            };

            SeedDbWith(item1);

            var result = await dataService.FetchAsync(1);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Tag.Id == 1);
            Assert.IsTrue(result.Tag.Color == "color");
        }

        [TestMethod]
        public async Task FetchAsync_IncludesDataSeries()
        {
            var dataPoint1 = new DataPoint { DataSeriesId = 1, Id = 1, Value = 1 };

            SeedDbWith(dataPoint1);

            var item1 = new DataSeries
            {
                Id = 1,
                Tag = new Tag(),
            };

            SeedDbWith(item1);

            var result = await dataService.FetchAsync(1);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.DataPoints.Count == 1);
            Assert.IsTrue(result.DataPoints.First().Value == 1);
        }

        [TestMethod]
        public async Task FetchAllAsync_IncludesTag()
        {
            var tag1 = new Tag { Id = 1, Color = "color" };

            SeedDbWith(tag1);

            var item1 = new DataSeries
            {
                Id = 1,
                TagId = 1,
            };

            SeedDbWith(item1);

            var results = await dataService.FetchAllAsync();

            var result = results.FirstOrDefault(ds => ds.Id == 1);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Tag.Id == 1);
            Assert.IsTrue(result.Tag.Color == "color");
        }

        [TestMethod]
        public async Task FetchAllAsync_IncludesDataSeries()
        {
            var dataPoint1 = new DataPoint { DataSeriesId = 1, Id = 1, Value = 1 };

            SeedDbWith(dataPoint1);

            var item1 = new DataSeries
            {
                Id = 1,
                Tag = new Tag(),
            };

            SeedDbWith(item1);

            var results = await dataService.FetchAllAsync();

            var result = results.FirstOrDefault(ds => ds.Id == 1);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.DataPoints.Count == 1);
            Assert.IsTrue(result.DataPoints.First().Value == 1);
        }

        protected override DataSeries AlteredCopy(DataSeries stub)
        {
            var dataSeries = new DataSeries
            {
                Id = stub.Id,
                Title = "Title",
            };

            return dataSeries;
        }

        protected override DataSeries CreateStub(int id = 0)
        {
            var dataSeries = new DataSeries
            {
                Id = id,
                DataPoints = new Collection<DataPoint>(),
                Tag = new Tag()
            };

            return dataSeries;
        }

        protected override DataSeriesService CreateTestableService(IServiceProvider sp)
        {
            return new DataSeriesService(sp);
        }
    }
}
