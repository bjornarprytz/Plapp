using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.ContentViews;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : BaseContentPage<IApplicationViewModel>
    {
        private readonly CollectionView _topics = new ();
        public IndexPage(IApplicationViewModel viewModel) : base(viewModel) // TODO: This throws exception (somewhere in DI, probably), due to missing Default Constructor
        {
            Content = _topics;
        }
        
        

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, vm => vm.Topics, page => page._topics.ItemsSource)
                .DisposeWith(bindingsDisposable);
            
            
        }
    }
}