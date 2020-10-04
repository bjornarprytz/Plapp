using Plapp.Core;
using System;

namespace Plapp
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }

        public IDataSeriesViewModel DataSeries { get; set; }
    }
}
