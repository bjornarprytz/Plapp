using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class TopicDetailsPage : BaseContentPage<ITopicViewModel>
    {
        private readonly Button _button = new();
        
        public TopicDetailsPage()
        {
            Content = _button;
        }

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            // No bindings yet
        }

    }
}