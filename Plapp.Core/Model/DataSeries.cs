
using System.Collections.Generic;

namespace Plapp.Core
{
    public class DataSeries
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<DataPoint> DataPoints { get; set; }


        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public string TagKey { get; set; }
        public Tag Tag { get; set; }
    }
}
