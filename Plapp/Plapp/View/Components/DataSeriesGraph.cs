
using Microcharts;
using Microcharts.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;

namespace Plapp
{
    public class DataSeriesGraph : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesGraph()
        {
            Content = new ChartView().Bind(ChartView.ChartProperty, nameof(VM.DataPoints), converter: new DataSeriesToChartConverter<LineChart>());
        }
    }
}
