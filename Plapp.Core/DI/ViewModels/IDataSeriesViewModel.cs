using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IDataSeriesViewModel : IViewModel
    {
        int Id { get; }

        ITopicViewModel Topic { get; }
        ITagViewModel Tag { get; set; }
        ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        ICommand LoadDataCommand { get; }
        ICommand AddDataPointCommand { get; }

        void AddDataPoint(IDataPointViewModel dataPoint);
        void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints);

    }
}
