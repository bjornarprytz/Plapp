using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.Relational
{
    public class PlappDataStore : IPlappDataStore
    {
        private readonly PlappDbContext _database;

        public PlappDataStore(PlappDbContext dbContext)
        {
            _database = dbContext;
        }

        public Task<ITagViewModel> CreateTagAsync(string tagId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ITopicViewModel> CreateTopicAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IDataSeriesViewModel>> FetchDataSeriesAsync(int? topicId = null, int? tagId = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<INoteViewModel>> FetchNotesAsync(int? topicId = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ITagViewModel>> FetchTagsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ITopicViewModel>> FetchTopicsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveDataSeriesAsync(IEnumerable<IDataSeriesViewModel> dataSeriesViewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveTagAsync(ITagViewModel tagViewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveTopicAsync(ITopicViewModel topicViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
