using System;

namespace Plapp
{
    public class DataPoint : DataTable
    {
        public int DataSeriesId { get; set; }
        public DateTime Date { get; set; }
        public object Value { get; set; }
    }
}
