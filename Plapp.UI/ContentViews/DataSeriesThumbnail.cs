using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using Microcharts;
using Microcharts.Forms;
using Plapp.Core;
using Plapp.UI.Converters;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Markup;

namespace Plapp.UI.ContentViews
{
    public class DataSeriesThumbnail : BaseContentView<IDataSeriesViewModel>
    {
        private readonly Image _dataIcon = new();
        private readonly ChartView _chart = new();
        private readonly Image _chevron = new();
        private readonly TapGestureRecognizer _tapGesture = new();
        
        public DataSeriesThumbnail()
        {
            Content = new Grid
            {
                ColumnDefinitions = GridRowsColumns.Columns.Define(GridLength.Auto, GridLength.Star, GridLength.Auto),
                RowDefinitions = GridRowsColumns.Rows.Define(GridLength.Auto),
                
                GestureRecognizers = { _tapGesture },
                
                Children =
                {
                    _dataIcon.Column(0),
                    _chart.Column(1),
                    _chevron.Column(2),
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, dataSeries => dataSeries.Tag.IconUri, thumbnail => thumbnail._dataIcon.Source, StringTo.ImageSource)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, dataSeries => dataSeries.DataPoints, thumbnail => thumbnail._chart.Chart, DataPointsTo.Chart<LineChart>)
                .DisposeWith(bindingsDisposable);

            this.BindCommand(ViewModel, dataSeries => dataSeries.OpenCommand, thumbnail => thumbnail._tapGesture)
                .DisposeWith(bindingsDisposable);
        }
    }
}