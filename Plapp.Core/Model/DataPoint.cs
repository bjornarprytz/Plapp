using System;

namespace Plapp.Core
{
    public record DataPoint
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }

        public int DataSeriesId { get; set; }
        public DataSeries DataSeries { get; set; }
    }
}
