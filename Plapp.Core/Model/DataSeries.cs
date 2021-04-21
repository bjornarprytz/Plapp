
using System.Collections.Generic;

namespace Plapp.Core
{
    public record DataSeries
    {
        public int Id { get; init; }
        public string Title { get; init; }

        public ICollection<DataPoint> DataPoints { get; init; }

        public int TopicId { get; init; }
        public Topic Topic { get; init; }

        public int TagId { get; init; }
        public Tag Tag { get; init; }
    }
}
