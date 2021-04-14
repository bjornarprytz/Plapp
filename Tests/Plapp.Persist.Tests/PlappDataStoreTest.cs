using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.Persist.Tests
{
    [TestClass]
    public class PlappDataStoreTest
    {
        PlappDataStore plappDataStore;
        Mock<IServiceProvider> serviceProviderMock;
        readonly DbContextOptions<PlappDbContext> options = new DbContextOptionsBuilder<PlappDbContext>()
                                                    .UseInMemoryDatabase(databaseName: "PlappDb")
                                                    .Options;

        [TestInitialize]
        public void Initialize()
        {
            serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(PlappDbContext)))).Returns(() => new PlappDbContext(options));

            plappDataStore = new PlappDataStore(serviceProviderMock.Object);

            DeleteDatabase();
        }

        [TestMethod]
        public async Task FetchTopicsAsyncReturnsAllTopics()
        {
            SeedTopics(
                new Topic { Id = TOPIC_ID },
                new Topic { Id = TOPIC_ID + 1 });

            var topics = await plappDataStore.FetchTopicsAsync();

            Assert.IsTrue(topics.Count() == 2);
            Assert.IsTrue(topics.Any(t => t.Id == TOPIC_ID));
            Assert.IsTrue(topics.Any(t => t.Id == TOPIC_ID + 1));
        }

        [TestMethod]
        public async Task FetchDataSeriesAsync_NoFilters_ShouldReturnAllDataSeries()
        {
            SeedDataSeries(
                new DataSeries { Id = DATASERIES_ID },
                new DataSeries { Id = DATASERIES_ID + 1 });

            var dataSeries = await plappDataStore.FetchDataSeriesAsync();

            Assert.IsTrue(dataSeries.Count() == 2);
            Assert.IsTrue(dataSeries.Any(ds => ds.Id == DATASERIES_ID));
            Assert.IsTrue(dataSeries.Any(ds => ds.Id == DATASERIES_ID + 1));
        }

        [TestMethod]
        public async Task FetchDataSeriesAsync_TagFilters()
        {
            const string TAG_FILTER = TAG_KEY;

            SeedDataSeries(
                new DataSeries { Id = TOPIC_ID, TagKey = TAG_FILTER },
                new DataSeries { Id = 2, TagKey = "earth" },
                new DataSeries { Id = 3 });

            var dataSeries = await plappDataStore.FetchDataSeriesAsync(tagKey: TAG_FILTER);

            Assert.IsTrue(dataSeries.Count() == 1);
            Assert.IsTrue(dataSeries.Any(ds => ds.Id == TOPIC_ID));
        }

        [TestMethod]
        public async Task FetchDataSeriesAsync_TopicFilters()
        {
            const int TOPIC_FILTER = TOPIC_ID;

            SeedDataSeries(
                new DataSeries { Id = DATASERIES_ID, TopicId = TOPIC_FILTER },
                new DataSeries { Id = 2, TopicId = 2 },
                new DataSeries { Id = 3 });

            var dataSeries = await plappDataStore.FetchDataSeriesAsync(topicId: TOPIC_FILTER);

            Assert.IsTrue(dataSeries.Count() == 1);
            Assert.IsTrue(dataSeries.Any(ds => ds.Id == DATASERIES_ID));
        }

        [TestMethod]
        public async Task FetchDataSeriesAsync_TopicAndTagFilters()
        {
            const int TOPIC_FILTER = TOPIC_ID;
            const string TAG_FILTER = TAG_KEY;

            SeedDataSeries(
                new DataSeries { Id = DATASERIES_ID, TopicId = TOPIC_FILTER, TagKey = TAG_FILTER },
                new DataSeries { Id = DATASERIES_ID + 1, TopicId = TOPIC_FILTER, TagKey = "earth" },
                new DataSeries { Id = DATASERIES_ID + 2, TopicId = 2, TagKey = TAG_FILTER });

            var dataSeries = await plappDataStore.FetchDataSeriesAsync(topicId: TOPIC_FILTER, tagKey: TAG_FILTER);

            Assert.IsTrue(dataSeries.Count() == 1);
            Assert.IsTrue(dataSeries.Any(ds => ds.Id == DATASERIES_ID));
        }

        [TestMethod]
        public async Task FetchDataPointsAsync_ShouldReturnExactlyAllDataPointsInSeries()
        {
            var seedPoints = new List<DataPoint> 
            { 
                new DataPoint { Id = DATAPOINT_ID, DataSeriesId = DATASERIES_ID }, 
                new DataPoint { Id = DATAPOINT_ID + 1, DataSeriesId = DATASERIES_ID } 
            };

            var otherSeedPoints = new List<DataPoint> 
            { 
                new DataPoint { Id = DATAPOINT_ID + 2, DataSeriesId = DATASERIES_ID + 1 }, 
                new DataPoint { Id = DATAPOINT_ID + 3, DataSeriesId = DATASERIES_ID + 1 } 
            };

            var seedSeries = new DataSeries { Id = DATASERIES_ID, DataPoints = seedPoints };
            var otherSeedSeries = new DataSeries { Id = DATASERIES_ID + 1, DataPoints = otherSeedPoints };

            SeedDataSeries(
                seedSeries,
                otherSeedSeries);

            var dataPoints = await plappDataStore.FetchDataPointsAsync(dataSeriesId: 1);

            Assert.IsTrue(dataPoints.Count() == 2);
            Assert.IsTrue(dataPoints.All(ds => ds.DataSeriesId == DATASERIES_ID));
        }

        [TestMethod]
        public async Task FetchTagsAsync_ShouldReturnAllTags()
        {
            SeedTags(
                new Tag { Id = TAG_ID },
                new Tag { Id = TAG_ID + 1 });

            var tags = await plappDataStore.FetchTagsAsync();

            Assert.IsTrue(tags.Count() == 2);
            Assert.IsTrue(tags.Any(ds => ds.Id == TAG_ID));
            Assert.IsTrue(tags.Any(ds => ds.Id == TAG_ID + 1));
        }

        [TestMethod]
        public async Task FetchTagAsync_ShouldReturnTagWithKey()
        {
            SeedTags(
                new Tag { Id = TAG_ID, Key = TAG_KEY },
                new Tag { Id = TAG_ID + 1, Key = "earth" });

            var tag = await plappDataStore.FetchTagAsync(TAG_KEY);

            Assert.IsTrue(tag.Id == TAG_ID);
            Assert.IsTrue(tag.Key == TAG_KEY);
        }

        [TestMethod]
        public async Task SaveDataSeriesAsync_NewDataSeries_ShouldAddDataSeriesAndDataPoints()
        {
            var dataPoints = new List<DataPoint>
            {
                new DataPoint { Id = DATAPOINT_ID, DataSeriesId = DATASERIES_ID },
                new DataPoint { Id = DATAPOINT_ID + 1, DataSeriesId = DATASERIES_ID }
            };

            var dataSeries = new DataSeries { Id = 1, DataPoints = dataPoints };

            await plappDataStore.SaveDataSeriesAsync(dataSeries);

            var resultSeries = await plappDataStore.FetchDataSeriesAsync();
            var resultPoints = await plappDataStore.FetchDataPointsAsync(dataSeriesId: 1);

            Assert.IsTrue(resultSeries.Count() == 1);

            Assert.IsTrue(resultPoints.Count() == 2);
            Assert.IsTrue(resultPoints.Any(dp => dp.Id == DATAPOINT_ID));
            Assert.IsTrue(resultPoints.Any(dp => dp.Id == DATAPOINT_ID + 1));
            Assert.IsTrue(resultPoints.All(dp => dp.DataSeriesId == DATASERIES_ID));
        }

        [TestMethod]
        public async Task SaveDataSeriesAsync_NewDataSeries_ShouldAddMultipleDataSeriesAndDataPoints()
        {
            var dataPoints = new List<DataPoint>
            {
                new DataPoint { Id = DATAPOINT_ID, DataSeriesId = DATASERIES_ID },
                new DataPoint { Id = DATAPOINT_ID + 1, DataSeriesId = DATASERIES_ID }
            };

            var otherDataPoints = new List<DataPoint>
            {
                new DataPoint { Id = DATAPOINT_ID + 2, DataSeriesId = DATASERIES_ID + 1 }
            };

            var dataSeries = new DataSeries { Id = DATASERIES_ID, DataPoints = dataPoints };
            var otherDataSeries = new DataSeries { Id = DATASERIES_ID + 1, DataPoints = otherDataPoints };

            await plappDataStore.SaveDataSeriesAsync(new List<DataSeries> { dataSeries, otherDataSeries });

            var resultSeries = await plappDataStore.FetchDataSeriesAsync();
            var resultPoints = await plappDataStore.FetchDataPointsAsync(dataSeriesId: DATASERIES_ID);
            var otherResultPoints = await plappDataStore.FetchDataPointsAsync(dataSeriesId: DATASERIES_ID + 1);

            Assert.IsTrue(resultSeries.Count() == 2);

            Assert.IsTrue(resultPoints.Count() == 2);
            Assert.IsTrue(resultPoints.Any(dp => dp.Id == DATAPOINT_ID));
            Assert.IsTrue(resultPoints.Any(dp => dp.Id == DATAPOINT_ID + 1));
            Assert.IsTrue(resultPoints.All(dp => dp.DataSeriesId == DATASERIES_ID));

            Assert.IsTrue(otherResultPoints.Count() == 1);
            Assert.IsTrue(otherResultPoints.Any(dp => dp.Id == DATAPOINT_ID + 2));
            Assert.IsTrue(otherResultPoints.All(dp => dp.DataSeriesId == DATASERIES_ID + 1));
        }

        [TestMethod]
        public async Task SaveTagAsync_NewTag_ShouldAdd()
        {
            await plappDataStore.SaveTagAsync(new Tag { Id = TAG_ID, Key = TAG_KEY });

            var tag = await plappDataStore.FetchTagAsync(tagKey: TAG_KEY);

            Assert.IsTrue(tag.Id == TAG_ID);
            Assert.IsTrue(tag.Key == TAG_KEY);
        }

        [TestMethod]
        public async Task SaveTagAsync_ExistingTag_ShouldUpdate()
        {
            SeedTags(new Tag { Id = TAG_ID });

            await plappDataStore.SaveTagAsync(new Tag { Id = TAG_ID, Key = TAG_KEY });

            var tag = await plappDataStore.FetchTagAsync(tagKey: TAG_KEY);

            Assert.IsTrue(tag.Id == TAG_ID);
            Assert.IsTrue(tag.Key == TAG_KEY);
        }

        [TestMethod]
        public async Task SaveTopicAsync_NewTopic_ShouldAdd()
        {
            await plappDataStore.SaveTopicAsync(new Topic { Id = TOPIC_ID });

            var topics = await plappDataStore.FetchTopicsAsync();

            Assert.IsTrue(topics.Any(t => t.Id == TOPIC_ID));
        }

        [TestMethod]
        public async Task SaveTopicAsync_ExistingTopic_ShouldUpdate()
        {
            SeedTopics(new Topic { Id = TOPIC_ID });

            await plappDataStore.SaveTopicAsync(new Topic 
            { 
                Id = TOPIC_ID, 
                Title = TOPIC_TITLE,
                Description = TOPIC_DESCRIPTION,
                ImageUri = TOPIC_IMAGE,
            });

            var topics = await plappDataStore.FetchTopicsAsync();

            var topic = topics.FirstOrDefault(t => t.Id == TOPIC_ID);

            Assert.IsNotNull(topic);
            Assert.IsTrue(topic.Id == TOPIC_ID);
            Assert.IsTrue(topic.Title == TOPIC_TITLE);
            Assert.IsTrue(topic.Description == TOPIC_DESCRIPTION);
            Assert.IsTrue(topic.ImageUri == TOPIC_IMAGE);
        }

        [TestMethod]
        public async Task SaveTopicsAsync_SomeExistingTopics_ShouldAddNewAndUpdateExisting()
        {
            SeedTopics(new Topic { Id = TOPIC_ID });

            var newTopics = new List<Topic>
            {
                new Topic
                {
                    Id = TOPIC_ID,
                    Title = TOPIC_TITLE,
                },
                new Topic { Id = TOPIC_ID+1 }
            };


            await plappDataStore.SaveTopicsAsync(newTopics);

            var topics = await plappDataStore.FetchTopicsAsync();

            var existingTopic = topics.FirstOrDefault(t => t.Id == TOPIC_ID);

            Assert.IsNotNull(existingTopic);
            Assert.IsTrue(existingTopic.Title == TOPIC_TITLE);

            Assert.IsTrue(topics.Any(t => t.Id == TOPIC_ID + 1));
        }

        [TestMethod]
        public async Task DeleteDataPointAsync_TwoDataPoints_OneIsDeleted()
        {
            var dataPoint1 = new DataPoint { Id = DATAPOINT_ID, DataSeriesId = DATASERIES_ID };
            var dataPoint2 = new DataPoint { Id = DATAPOINT_ID + 1, DataSeriesId = DATASERIES_ID };

            var seedPoints = new List<DataPoint>
            {
                dataPoint1,
                dataPoint2
            };

            var seedSeries = new DataSeries { Id = DATASERIES_ID, DataPoints = seedPoints };

            SeedDataSeries(seedSeries);

            await plappDataStore.DeleteDataPointAsync(dataPoint1);

            var dataPoints = await plappDataStore.FetchDataPointsAsync(dataSeriesId: DATASERIES_ID);

            Assert.IsFalse(dataPoints.Any(dp => dp.Id == DATAPOINT_ID));
            Assert.IsTrue(dataPoints.Any(dp => dp.Id == DATAPOINT_ID + 1));
        }

        [TestMethod]
        public async Task DeleteDataSeriesAsync_AllDataPointsAreDeleted()
        {
            var dataPoint1 = new DataPoint { Id = DATAPOINT_ID, DataSeriesId = DATASERIES_ID };
            var dataPoint2 = new DataPoint { Id = DATAPOINT_ID + 1, DataSeriesId = DATASERIES_ID };

            var seedPoints = new List<DataPoint>
            {
                dataPoint1,
                dataPoint2
            };

            var seedSeries = new DataSeries { Id = DATASERIES_ID, DataPoints = seedPoints };

            SeedDataSeries(seedSeries);

            await plappDataStore.DeleteDataSeriesAsync(seedSeries);

            var dataPoints = await plappDataStore.FetchDataPointsAsync(dataSeriesId: DATASERIES_ID);

            Assert.IsFalse(dataPoints.Any());
        }

        [TestMethod]
        public async Task DeleteTagAsync_ItIsDeleted()
        {
            var seedTag = new Tag { Id = TAG_ID, Key = TAG_KEY };

            SeedTags(seedTag);

            await plappDataStore.DeleteTagAsync(seedTag);

            var tag = await plappDataStore.FetchTagAsync(tagKey: TAG_KEY);

            Assert.IsNull(tag);
        }

        [TestMethod]
        public async Task DeleteTagAsync_DataSeriesWithTagKey_DataSeriesIsNotDeleted()
        {
            var seedTag = new Tag { Id = TAG_ID, Key = TAG_KEY };

            var seedSeries = new DataSeries { Id = DATASERIES_ID, TagKey = TAG_KEY };

            SeedTags(seedTag);

            SeedDataSeries(seedSeries);

            await plappDataStore.DeleteTagAsync(seedTag);

            var dataSeries = await plappDataStore.FetchDataSeriesAsync(tagKey: TAG_KEY);

            var singleSeries = dataSeries.FirstOrDefault();

            Assert.IsNotNull(singleSeries);
            Assert.IsTrue(singleSeries.TagKey == TAG_KEY);
        }

        [TestMethod]
        public async Task DeleteTopicAsync_ItIsDeleted()
        {
            var seedTopic = new Topic { Id = TOPIC_ID };

            SeedTopics(seedTopic);

            await plappDataStore.DeleteTopicAsync(seedTopic);

            var topics = await plappDataStore.FetchTopicsAsync();

            Assert.IsFalse(topics.Any(t => t.Id == TOPIC_ID));
        }


        private void SeedTopics(params Topic[] topics)
        {
            using var context = new PlappDbContext(options);

            context.Topics.AddRange(topics);

            context.SaveChanges();
        }

        private void SeedDataSeries(params DataSeries[] series)
        {
            using var context = new PlappDbContext(options);

            context.DataSeries.AddRange(series);

            context.SaveChanges();
        }

        private void SeedDataPoints(params DataPoint[] points)
        {
            using var context = new PlappDbContext(options);

            context.DataPoints.AddRange(points);

            context.SaveChanges();
        }

        private void SeedTags(params Tag[] tags)
        {
            using var context = new PlappDbContext(options);

            context.Tags.AddRange(tags);

            context.SaveChanges();
        }

        private void DeleteDatabase()
        {
            using var context = new PlappDbContext(options);

            context.Database.EnsureDeleted();
        }

        const int TOPIC_ID = 1;
        const string TOPIC_DESCRIPTION = "some description";
        const string TOPIC_IMAGE = "image.jpg";
        const string TOPIC_TITLE = "some title";

        const int DATASERIES_ID = 1;

        const int DATAPOINT_ID = 1;

        const int TAG_ID = 1;
        const string TAG_KEY = "water";
    }
}
