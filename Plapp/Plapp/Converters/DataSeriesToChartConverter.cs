using Microcharts;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Plapp
{
    public class DataSeriesToChartConverter<T> : BaseValueConverter
        where T : Chart, new()
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable<IDataPointViewModel>))
            {
                return null;
            }

            var datapoints = value as IEnumerable<IDataPointViewModel>;

            var chart = new T
            {
                Entries = datapoints.OrderBy(dp => dp.Date).Select(dp => DataPointToChartEntry(dp))
            };

            return chart;
        }

        private ChartEntry DataPointToChartEntry(IDataPointViewModel dataPointViewModel)
        {
            return new ChartEntry(dataPointViewModel.Value);
        }
    }
}
