using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class DataSeriesDetailsPage : BaseContentPage<IDataSeriesViewModel>
    {
        private readonly Button _button = new(){ Text = "Data series" };
        
        public DataSeriesDetailsPage()
        {
            Content = _button;
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(this.ViewModel, dataSeries => dataSeries.Tag.Key, page => page._button.Text)
                .DisposeWith(bindingsDisposable);
        }
    }
}