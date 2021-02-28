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
        private readonly Func<PlappDbContext> _dbContext;
        private PlappDbContext Context => _dbContext();

        public PlappDataStore(Func<PlappDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EnsureStorageReadyAsync(CancellationToken cancellationToken)
        {
            if (!(await Context.Database.EnsureCreatedAsync(cancellationToken)))
                return false;

            return true;
        }

        public async Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId = null, string tagId = null, string dataSeriesTitle = null)
        {
            var result = Context.DataSeries.Where(
                d => (tagId == null || d.TagId == tagId)
                  && (dataSeriesTitle == null || d.Title == dataSeriesTitle)
                  && (topicId == null || d.TopicId == topicId));

             return await result.ToListAsync();
        }

        public async Task<Tag> FetchTagAsync(string tagId)
        {
            return await Context.Tags.FindAsync(tagId);
        }

        public async Task<IEnumerable<Tag>> FetchTagsAsync()
        {
            var tags = await Context.Tags.ToListAsync();

            return tags;
        }

        public async Task<IEnumerable<Topic>> FetchTopicsAsync()
        {
            return await Context.Topics.ToListAsync();
        }

        public async Task SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries)
        {
            await Context.DataSeries.AddRangeAsync(dataSeries);

            await Context.SaveChangesAsync();
        }

        public async Task SaveDataSeriesAsync(DataSeries dataSeries)
        {
            await Context.DataSeries.AddAsync(dataSeries);

            await Context.SaveChangesAsync();
        }

        public async Task SaveTagAsync(Tag tag)
        {
            Context.Tags.Update(tag);

            await Context.SaveChangesAsync();
        }

        public async Task SaveTopicAsync(Topic topic)
        {
            Context.Topics.Update(topic);

            await Context.SaveChangesAsync();
        }

        public async Task SaveTopicsAsync(IEnumerable<Topic> topics)
        {
            Context.Topics.UpdateRange(topics);

            await Context.SaveChangesAsync();
        }

        public async Task DeleteDataPointAsync(DataPoint dataPoint)
        {
            Context.DataPoints.Remove(dataPoint);

            await Context.SaveChangesAsync();
        }

        public async Task DeleteDataSeriesAsync(DataSeries dataSeries)
        {
            Context.DataSeries.Remove(dataSeries);

            await Context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Tag tag)
        {
            Context.Tags.Remove(tag);

            await Context.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(Topic topic)
        {
            Context.Topics.Remove(topic);

            await Context.SaveChangesAsync();
        }
    }
}
