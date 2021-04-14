using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public class PlappDataStore : IPlappDataStore
    {
        private readonly IServiceProvider _serviceProvider;
        

        public PlappDataStore(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> EnsureStorageReadyAsync(CancellationToken cancellationToken)
        {
            using var context = await GetContextAsync(cancellationToken);

            if (!(await context.Database.EnsureCreatedAsync(cancellationToken)))
                return false;

            return true;
        }

        public async Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId = null, string tagKey = null, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            var result = context.DataSeries.Where(
                d => (tagKey == null || d.TagKey == tagKey)
                  && (topicId == null || d.TopicId == topicId));

             return await result
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<DataPoint>> FetchDataPointsAsync(int dataSeriesId, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            var result = context.DataPoints.Where(d => d.DataSeriesId == dataSeriesId);

            return await result
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Tag> FetchTagAsync(string tagKey, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            var result = context.Tags.Where(d => d.Key == tagKey);

            return await result
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Tag>> FetchTagsAsync(CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);
            
            return await context.Tags
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Topic>> FetchTopicsAsync(CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);
            
            return await context.Topics
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            await context.DataSeries.AddRangeAsync(dataSeries, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveDataSeriesAsync(DataSeries dataSeries, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            await context.DataSeries.AddAsync(dataSeries, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveTagAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            await context.Tags.AddOrUpdate(tag.Id, tag);
            
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveTopicAsync(Topic topic, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);
            
            await context.Topics.AddOrUpdate(topic.Id, topic);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveTopicsAsync(IEnumerable<Topic> topics, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            var addOrUpdateTasks = topics.Select(t => context.Topics.AddOrUpdate(t.Id, t));

            await Task.WhenAll(addOrUpdateTasks);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDataPointAsync(DataPoint dataPoint, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);
            
            context.DataPoints.Remove(dataPoint);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDataSeriesAsync(DataSeries dataSeries, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);
            
            context.DataSeries.Remove(dataSeries);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTagAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);
            
            context.Tags.Remove(tag);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTopicAsync(Topic topic, CancellationToken cancellationToken = default)
        {
            using var context = await GetContextAsync(cancellationToken);

            context.Topics.Remove(topic);
            
            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task<PlappDbContext> GetContextAsync(CancellationToken cancellationToken = default)
        {
            var context = _serviceProvider.Get<PlappDbContext>();

            await context.Database.EnsureCreatedAsync(cancellationToken);

            return context;
        }
    }
}
