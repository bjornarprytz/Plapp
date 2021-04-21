using System.Collections.Generic;

namespace Plapp.Core
{
    public record Topic
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string ImageUri { get; init; }

        public ICollection<DataSeries> DataSeries { get; init; }
    }
}
