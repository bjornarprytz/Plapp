using System;

namespace Plapp.Core
{
    public class DataPoint : DomainObject
    {
        public DateTime Date { get; set; }
        public long Value { get; set; }

        public int DataSeriesId { get; set; }
        public DataSeries DataSeries { get; set; }
    }
}
