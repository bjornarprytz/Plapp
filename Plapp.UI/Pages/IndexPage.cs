using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : BaseContentPage<IApplicationViewModel>
    {
        private Button button = new Button();
        

        private CollectionView topics = new CollectionView();
        
        public IndexPage()
        {
            Content = topics;
        }

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, vm => vm.Topics, page => page.topics.ItemsSource)
                .DisposeWith(bindingsDisposable);
        }
    }
}