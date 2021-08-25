using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.ContentViews;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : BaseContentPage<IApplicationViewModel>
    {
        private readonly CollectionView _topics = new();
        public IndexPage()
        {
            _topics.ItemTemplate = new DataTemplate(() =>  new Label{Text = "Yo"});
            
            Content = _topics;
        }
        
        

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, vm => vm.Topics, page => page._topics.ItemsSource)
                .DisposeWith(bindingsDisposable);
            
            
        }
    }
}