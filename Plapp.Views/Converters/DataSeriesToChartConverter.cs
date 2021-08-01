using System;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using Plapp.Core;
using SkiaSharp;

namespace Plapp.Views.Converters
{
    public class DataSeriesToChartConverter<T> : BaseValueConverter<IEnumerable<IDataPointViewModel>, T>
        where T : Chart, new()
    {
        readonly Action<T> _setState;

        public DataSeriesToChartConverter(Action<T> setState=null)
        {
            _setState = setState;
        }

        protected override T Convert(IEnumerable<IDataPointViewModel> from)
        {
            var chart = new T
            {
                Entries = from.OrderBy(dp => dp.Date).Select(dp => DataPointToChartEntry(dp)),
                BackgroundColor = SKColors.Transparent,
            };

            _setState?.Invoke(chart);

            return chart;
        }

        private ChartEntry DataPointToChartEntry(IDataPointViewModel dataPointViewModel)
        {
            return new ChartEntry(dataPointViewModel.Value)
            {
                Color = SKColors.White,
                TextColor = SKColors.White,
                Label = "",
                ValueLabel = dataPointViewModel.Value.ToString(),
            };
        }
    }
}
