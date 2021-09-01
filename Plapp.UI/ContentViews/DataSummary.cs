using System.Reactive.Disposables;
using Microcharts.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Plapp.UI.Extensions;
using ReactiveUI;

namespace Plapp.UI.ContentViews
{
    public class DataSummary : BaseContentView<ITopicViewModel>
    {
        private readonly Label _overview = new(); // TODO: Turn this into a multi-graph chart (show all data series overlapped)
        private readonly Image _chevron = new();

        private readonly CollectionView _dataSeries = new();

        
        private readonly TapGestureRecognizer _tapGesture = new();
        
        public DataSummary()
        {
            Content = new Expander
            {
                Header = new Grid
                {
                    ColumnDefinitions = GridRowsColumns.Columns.Define(GridLength.Star, GridLength.Auto),
                    RowDefinitions = GridRowsColumns.Rows.Define(GridLength.Auto),

                    GestureRecognizers = { _tapGesture },

                    Children =
                    {
                        _overview.Column(0),
                        _chevron.Column(1)
                    }
                },

                Content = _dataSeries
                    .ItemTemplate(() => new DataSeriesGraph())
                    .ItemsLayout(new LinearItemsLayout(ItemsLayoutOrientation.Vertical))
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, topic => topic.DataSeries, summary => summary._dataSeries.ItemsSource)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, topic => topic.Title, summary => summary._overview.Text)
                .DisposeWith(bindingsDisposable);
        }
    }
}