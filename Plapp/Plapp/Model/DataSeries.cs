using System.Collections.Generic;

namespace Plapp
{
    public class DataSeries
    {
        public string Tag { get; set; }
        public string Unit { get; set; }
        public List<DataPoint> DataPoints { get; set; }
    }
}
