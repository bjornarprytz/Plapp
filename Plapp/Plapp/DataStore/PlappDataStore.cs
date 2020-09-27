using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Plapp
{
    public class PlappDataStore : IPlappDataStore
    {
        private readonly SQLiteAsyncConnection _database;

        public PlappDataStore()
        {
            _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
        }

        public async Task<ITagViewModel> CreateTag(string name)
        {
            var tag = new Tag { Name = name };

            var id = await _database.InsertAsync(tag);

            return new TagViewModel { Id = id, Name = name };
        }

        public async Task<ITopicViewModel> CreateTopic()
        {
            var topic = new Topic();

            var id = await _database.InsertAsync(topic);

            return new TopicViewModel { Id = id };
        }

        public async Task<IEnumerable<IDataSeriesViewModel>> FetchDataSeries(int? topicId = null, int? tagId = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<INoteViewModel>> FetchNotes(int? topicId = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ITagViewModel>> FetchTags()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ITopicViewModel>> FetchTopicsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> SaveDataSeriesAsync(IDataSeriesViewModel dataSeriesViewModel)
        {
            return dataSeriesViewModel.Id != 0 ?
                  await _database.UpdateAsync(dataSeriesViewModel)
                : await _database.InsertAsync(dataSeriesViewModel);
        }

        public async Task<int> SaveTag(ITagViewModel tagViewModel)
        {
            return tagViewModel.Id != 0 ?
                  await _database.UpdateAsync(tagViewModel)
                : await _database.InsertAsync(tagViewModel);
        }

        public async Task<int> SaveTopicAsync(ITopicViewModel topicViewModel)
        {
            return topicViewModel.Id != 0 ?
                  await _database.UpdateAsync(topicViewModel)
                : await _database.InsertAsync(topicViewModel);
        }
    }
}
