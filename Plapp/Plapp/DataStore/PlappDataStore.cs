using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp
{
    public class PlappDataStore : IPlappDataStore
    {
        private readonly IPlappDatabase _database;

        public PlappDataStore()
        {
            _database = IoC.Get<IPlappDatabase>();
        }

        public async Task<ITagViewModel> CreateTagAsync(string id)
        {
            var tag = await _database.FetchTagAsync(id);

            if (tag == null)
            {
                tag = new Tag { Id = id };
                await _database.SaveTagAsync(tag);
            }
            
            return tag.ToViewModel();
            
        }

        public async Task<ITopicViewModel> CreateTopicAsync()
        {
            var topic = new Topic();

            var id = await _database.SaveTopicAsync(topic);

            return new TopicViewModel { Id = id };
        }

        public async Task<IEnumerable<IDataSeriesViewModel>> FetchDataSeriesAsync(int? topicId = null, int? tagId = null)
        {
            var dataSeries = await _database.FetchDataSeriesAsync(topicId, tagId);

            return dataSeries.Select(d => d.ToViewModel());
        }

        public async Task<IEnumerable<INoteViewModel>> FetchNotesAsync(int? topicId = null)
        {
            var notes = await _database.FetchNotesAsync(topicId);

            return notes.Select(n => n.ToViewModel());
        }

        public async Task<IEnumerable<ITagViewModel>> FetchTagsAsync()
        {
            var tags = await _database.FetchTagsAsync();

            return tags.Select(n => n.ToViewModel());
        }

        public async Task<IEnumerable<ITopicViewModel>> FetchTopicsAsync()
        {
            var topics = await _database.FetchTopicsAsync();

            return topics.Select(n => n.ToViewModel());
        }

        public async Task<int> SaveDataSeriesAsync(IEnumerable<IDataSeriesViewModel> dataSeriesViewModel)
        {
            return await _database.SaveDataSeriesAsync(dataSeriesViewModel.Select(d => d.ToModel()));
        }

        public async Task<bool> SaveTagAsync(ITagViewModel tagViewModel)
        {
            return await _database.SaveTagAsync(tagViewModel.ToModel());
        }

        public async Task<int> SaveTopicAsync(ITopicViewModel topicViewModel)
        {
            return await _database.SaveTopicAsync(topicViewModel.ToModel());
        }
    }
}
