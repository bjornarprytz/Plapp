
using System.Collections.Generic;

namespace Plapp.Core
{
    public class DataSeries
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public Tag Tag { get; set; }

        public ICollection<DataPoint> DataPoints { get; set; }
    }
}
