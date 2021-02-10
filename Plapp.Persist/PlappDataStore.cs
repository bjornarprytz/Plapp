using Microsoft.EntityFrameworkCore;
using PCLStorage;
using Plapp.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public class PlappDataStore : IPlappDataStore
    {
        private readonly PlappDbContext _dbContext;

        public PlappDataStore(PlappDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EnsureStorageReadyAsync(CancellationToken cancellationToken)
        {
            if (!(await _dbContext.Database.EnsureCreatedAsync(cancellationToken)))
                return false;

            return true;
        }

        public async Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId = null, string tagId = null)
        {
            var result = _dbContext.DataSeries.Where(
                d => (tagId == null || d.TagId == tagId)
                  && (topicId == null || d.TopicId == topicId));

             return await result.ToListAsync();
        }

        public async Task<Tag> FetchTagAsync(string tagId)
        {
            return await _dbContext.Tags.FindAsync(tagId);
        }

        public async Task<IEnumerable<Tag>> FetchTagsAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<IEnumerable<Topic>> FetchTopicsAsync()
        {
            return await _dbContext.Topics.ToListAsync();
        }

        public async Task SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries)
        {
            await _dbContext.DataSeries.AddRangeAsync(dataSeries);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveTagAsync(Tag tag)
        {
            _dbContext.Tags.Update(tag);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveTopicAsync(Topic topic)
        {
            _dbContext.Topics.Update(topic);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveTopicsAsync(IEnumerable<Topic> topics)
        {
            _dbContext.Topics.UpdateRange(topics);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDataPointAsync(DataPoint dataPoint)
        {
            _dbContext.DataPoints.Remove(dataPoint);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDataSeriesAsync(DataSeries dataSeries)
        {
            _dbContext.DataSeries.Remove(dataSeries);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Tag tag)
        {
            _dbContext.Tags.Remove(tag);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(Topic topic)
        {
            _dbContext.Topics.Remove(topic);

            await _dbContext.SaveChangesAsync();
        }
    }
}
