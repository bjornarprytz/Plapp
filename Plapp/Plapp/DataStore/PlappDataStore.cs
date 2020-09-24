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
            
            SetupTables().Wait();
        }

        public async Task SetupTables()
        {
            await _database.CreateTableAsync<Topic>();
            await _database.CreateTableAsync<DataPoint>();
            await _database.CreateTableAsync<DataSeries>();
            await _database.CreateTableAsync<Note>();
            await _database.CreateTableAsync<Tag>();
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

        public async Task SaveDataSeriesAsync(IDataSeriesViewModel dataSeriesViewModel)
        {
            if (dataSeriesViewModel.Id != 0)
            {
                await _database.UpdateAsync(dataSeriesViewModel);
            }
            else
            {
                await _database.InsertAsync(dataSeriesViewModel);
            }
        }

        public async Task SaveTag(ITagViewModel tagViewModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveTopicAsync(ITopicViewModel topicViewModel)
        {
            if (topicViewModel.Id != 0)
            {
                await _database.UpdateAsync(topicViewModel);
            }
            else
            {
                await _database.InsertAsync(topicViewModel);
            }
        }
    }
}
