using System.Collections.Generic;

namespace Plapp
{
    public class Topic
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<DataSeries> DataSeries { get; set; }
        public List<DiaryEntry> DiaryEntries { get; set; }
    }
}
