using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plapp
{
    public interface IPlappDatabase
    {
        Task<IEnumerable<Topic>> FetchTopicsAsync();
        Task<IEnumerable<DataSeries>> FetchDataSeriesAsync(int? topicId = null, int? tagId = null);
        Task<IEnumerable<Note>> FetchNotesAsync(int? topicId = null);
        Task<IEnumerable<Tag>> FetchTagsAsync();
        Task<Tag> FetchTagAsync(string tagId);

        Task<int> SaveTopicAsync(Topic topic);
        Task<int> SaveDataSeriesAsync(IEnumerable<DataSeries> dataSeries);
        Task<int> SaveNotesAsync(IEnumerable<Note> notes);
        Task<bool> SaveTagAsync(Tag tag);

        Task<int> DeleteTopicAsync(Topic topic);
        Task<int> DeleteDataSeriesAsync(DataSeries dataSeries);
        Task<int> DeleteDataPointAsync(DataPoint dataSeries);
        Task<int> DeleteNoteAsync(Note notes);
        Task<int> DeleteTag(Tag tag);

    }
}
