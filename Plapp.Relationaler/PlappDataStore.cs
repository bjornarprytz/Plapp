using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.Relational
{
    public class PlappDataStore : IPlappDataStore
    {
        private readonly PlappDbContext _dbContext;

        public PlappDataStore(PlappDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EnsureDbCreatedAsync()
        {
            return await _dbContext.Database.EnsureCreatedAsync();
        }

        public async Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId = null, string tagId = null)
        {
            var result = _dbContext.DataSeries.Where(
                d => (tagId == null || d.TagId == tagId)
                  && (topicId == null || d.TopicId == topicId));

             return await result.ToListAsync();
        }

        public async Task<IEnumerable<Note>> FetchNotesAsync(int? topicId = null)
        {
            var result = _dbContext.Notes.Where(n => topicId == null || n.TopicId == topicId);

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
        }

        public Task SaveTagAsync(Tag tag)
        {
            return Task.FromResult(_dbContext.Tags.Update(tag));
        }

        public Task SaveTopicAsync(Topic topic)
        {
            return Task.FromResult(_dbContext.Topics.Update(topic));
        }

        public Task SaveTopicsAsync(IEnumerable<Topic> topics)
        {
            _dbContext.Topics.UpdateRange(topics);

            return Task.FromResult(0);
        }

        public Task DeleteDataPointAsync(DataPoint dataPoint)
        {
            _dbContext.DataPoints.Remove(dataPoint);

            return Task.FromResult(0);
        }

        public Task DeleteDataSeriesAsync(DataSeries dataSeries)
        {
            _dbContext.DataSeries.Remove(dataSeries);

            return Task.FromResult(0);
        }

        public Task DeleteTagAsync(Tag tag)
        {
            _dbContext.Tags.Remove(tag);

            return Task.FromResult(0);
        }

        public Task DeleteTopicAsync(Topic topic)
        {
            _dbContext.Topics.Remove(topic);

            return Task.FromResult(0);
        }
    }
}
