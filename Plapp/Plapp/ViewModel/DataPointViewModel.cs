using Plapp.Core;
using System;

namespace Plapp
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public object Data { get; set; }

        public IDataSeriesViewModel DataSeries { get; set; }
    }
}
