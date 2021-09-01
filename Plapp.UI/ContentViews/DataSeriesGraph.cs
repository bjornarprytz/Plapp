using System.Reactive.Disposables;
using Microcharts;
using Microcharts.Forms;
using Plapp.Core;
using Plapp.UI.Converters;
using ReactiveUI;

namespace Plapp.UI.ContentViews
{
    public class DataSeriesGraph : BaseContentView<IDataSeriesViewModel>
    {
        private readonly ChartView _chart = new();

        public DataSeriesGraph()
        {
            Content = _chart;
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, dataSeries => dataSeries.DataPoints, graph => graph._chart.Chart, DataPointsTo.Chart<LineChart>)
                .DisposeWith(bindingsDisposable);
        }
    }
}