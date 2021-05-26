
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

                    Children =
                    {
                        new ChartView()
                            .HeightRequest(120)
                            .BindTapGesture(nameof(VM.AddDataPointCommand))
                            .Bind(IsVisibleProperty, nameof(VM.DataPoints), convert: (IEnumerable<IDataPointViewModel> c) => c.Any())
                            .Bind(ChartView.ChartProperty, nameof(VM.DataPoints), converter: new DataSeriesToChartConverter<LineChart>(
                                chart =>
                                {
                                    chart.LineMode = LineMode.Straight;
                                    chart.PointMode = PointMode.Circle;
                                    chart.LineSize = 1;
                                })),

                        new Button()
                            .BindCommand(nameof(VM.AddDataPointCommand))
                            .BackgroundColor(Color.Transparent)
                            .MaterialIcon(MaterialDesign.Icons.MaterialIcon.Add, color: Color.MediumPurple)
                            .Bind(IsVisibleProperty, nameof(VM.DataPoints), convert: (IEnumerable<IDataPointViewModel> c) => !c.Any())
                    }
                }.Bind(BackgroundColorProperty, BindingHelpers.BuildPath(nameof(VM.Tag), nameof(VM.Tag.Color)), converter: new StringToColorConverter());
        }
    }
}
