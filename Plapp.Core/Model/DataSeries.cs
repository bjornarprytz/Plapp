
using System.Collections.Generic;

namespace Plapp.Core
{
    public class DataSeries
    {
        public int Id { get; set; }
        public Tag Tag { get; set; }
        public List<DataPoint> DataPoints { get; set; }
    }
}
