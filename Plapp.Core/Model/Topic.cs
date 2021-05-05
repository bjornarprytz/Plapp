using System.Collections.Generic;

namespace Plapp.Core
{
    public class Topic : DomainObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public ICollection<DataSeries> DataSeries { get; set; }
    }
}
