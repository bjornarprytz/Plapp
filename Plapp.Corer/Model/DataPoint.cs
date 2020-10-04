using System;

namespace Plapp.Core
{
    public class DataPoint
    {
        public int Id { get; set; }
        public int DataSeriesId { get; set; }
        public DateTime Date { get; set; }
        public object Value { get; set; }

    }
}
