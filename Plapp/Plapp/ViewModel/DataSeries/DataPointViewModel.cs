using System;

namespace Plapp.ViewModel.DataSeries
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Data { get; set; }
    }
}
