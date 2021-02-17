
using Microcharts;
using Microcharts.Forms;
using Plapp.Core;
using SkiaSharp;

namespace Plapp
{
    public class DataSeriesGraph : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesGraph()
        {
            var entries = new[]
            {
                new ChartEntry(212)
                {
                    Label = "UWP",
                    ValueLabel = "112",
                    Color = SKColor.Parse("#2c3e50")
                },
                new ChartEntry(248)
                {
                    Label = "Android",
                    ValueLabel = "648",
                    Color = SKColor.Parse("#77d065")
                },
                new ChartEntry(128)
                {
                    Label = "iOS",
                    ValueLabel = "428",
                    Color = SKColor.Parse("#b455b6")
                },
                new ChartEntry(514)
                {
                    Label = "Forms",
                    ValueLabel = "214",
                    Color = SKColor.Parse("#3498db")
                }
            };
            var chart = new LineChart { Entries = entries };

            Content = new ChartView { Chart = chart };
        }
    }
}
