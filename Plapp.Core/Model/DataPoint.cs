using System;

namespace Plapp.Core
{
    public record DataPoint
    {
        public int Id { get; init; }
        public DateTime Date { get; init; }
        public long Value { get; init; }

        public int DataSeriesId { get; init; }
        public DataSeries DataSeries { get; init; }
    }
}
