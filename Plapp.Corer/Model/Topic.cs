using System.Collections.Generic;

namespace Plapp.Core
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<DataSeries> DataSeries { get; set; }
    }
}
