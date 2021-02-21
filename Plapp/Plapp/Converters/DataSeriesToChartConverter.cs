using Microcharts;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;

namespace Plapp
{
    public class DataSeriesToChartConverter<T> : BaseValueConverter<IEnumerable<IDataPointViewModel>, T>
        where T : Chart, new()
    {
        protected override T Convert(IEnumerable<IDataPointViewModel> from)
        {
            var chart = new T
            {
                Entries = from.OrderBy(dp => dp.Date).Select(dp => DataPointToChartEntry(dp))
            };

            return chart;
        }

        private ChartEntry DataPointToChartEntry(IDataPointViewModel dataPointViewModel)
        {
            return new ChartEntry(dataPointViewModel.Value);
        }
    }
}
