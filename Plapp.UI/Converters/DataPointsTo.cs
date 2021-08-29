using System.Collections.Generic;
using System.Linq;
using Microcharts;
using Plapp.Core;
using SkiaSharp;

namespace Plapp.UI.Converters
{
    public static class DataPointsTo
    {
        public static T Chart<T>(IEnumerable<IDataPointViewModel>? dataPoints)
        where T : Chart, new()
        {
            dataPoints ??= Enumerable.Empty<IDataPointViewModel>();
            
            return new T()
            {
                Entries = dataPoints.OrderBy(dp => dp.Date).Select(DataPointToChartEntry),
                BackgroundColor = SKColors.Transparent
            };
        }
        
        private static ChartEntry DataPointToChartEntry(IDataPointViewModel dataPointViewModel)
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