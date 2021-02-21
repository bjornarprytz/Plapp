
using System.Collections.Generic;

namespace Plapp.Core
{
    public class DataSeries
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TagId { get; set; }
        public int TopicId { get; set; }

        public ICollection<DataPoint> DataPoints { get; set; }
    }
}
