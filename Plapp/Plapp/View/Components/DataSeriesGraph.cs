
using Microcharts;
using Microcharts.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Plapp
{
    public class DataSeriesGraph : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesGraph()
        {
            Content =
                new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,

                    Children =
                    {
                        new ChartView()
                            .Bind(IsVisibleProperty, nameof(VM.DataPoints), convert: (IEnumerable<IDataPointViewModel> c) => c.Any())
                            .Bind(ChartView.ChartProperty, nameof(VM.DataPoints), converter: new DataSeriesToChartConverter<LineChart>()),
                        
                        new Button()
                            .MaterialIcon(MaterialDesign.Icons.MaterialIcon.Add)
                            .BindCommand(nameof(VM.AddDataPointCommand))
                    }
                };
        }
    }
}
