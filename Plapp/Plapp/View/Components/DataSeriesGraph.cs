
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
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.StartAndExpand,

                    Children =
                    {
                        new Label()
                            .NamedFontSize(NamedSize.Title)
                            .Bind(nameof(VM.Title)),
                        
                        new ChartView()
                            .MinHeight(120)
                            .Bind(IsVisibleProperty, nameof(VM.DataPoints), convert: (IEnumerable<IDataPointViewModel> c) => c.Any())
                            .Bind(ChartView.ChartProperty, nameof(VM.DataPoints), converter: new DataSeriesToChartConverter<LineChart>(
                                chart =>
                                {
                                    chart.LineMode = LineMode.Straight;
                                    chart.PointMode = PointMode.Circle;
                                    chart.LineSize = 1;
                                })),
                    }
                }.Bind(BackgroundColorProperty, BindingHelpers.BuildPath(nameof(VM.Tag), nameof(VM.Tag.Color)), converter: new StringToColorConverter());
        }
    }
}
