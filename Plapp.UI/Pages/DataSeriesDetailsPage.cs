using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.ContentViews;
using ReactiveUI;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class DataSeriesDetailsPage : BaseContentPage<IDataSeriesViewModel>
    {
        private readonly DataSeriesGraph _graph = new();

        private readonly Label _tag;
        private readonly TapGestureRecognizer _tapTagGesture = new();

        private readonly Slider _newDataPointSlider = new();
        private readonly Button _addDataPointButton = new();
        
        
        public DataSeriesDetailsPage()
        {
            _tag = new Label { GestureRecognizers = { _tapTagGesture } };
            
            Content = new StackLayout
            {
                Children =
                {
                    _graph,
                    _tag,
                    
                    _newDataPointSlider,
                    _addDataPointButton
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, dataSeries => dataSeries, page => page._graph.BindingContext);
            
            this.OneWayBind(ViewModel, dataSeries => dataSeries.Tag.Key, page => page._tag.Text)
                .DisposeWith(bindingsDisposable);

            this.BindCommand(ViewModel, dataSeries => dataSeries.PickTagCommand, page => page._tapTagGesture)
                .DisposeWith(bindingsDisposable);
            
            this.BindCommand(ViewModel, dataSeries => dataSeries.AddDataPointCommand, page => page._addDataPointButton)
                .DisposeWith(bindingsDisposable);

            
        }
    }
}