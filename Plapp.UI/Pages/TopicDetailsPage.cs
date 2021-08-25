using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class TopicDetailsPage : BaseContentPage<ITopicViewModel>
    {
        private Button button;
        
        public TopicDetailsPage()
        {
            button = new Button();

            Content = button;
        }

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            // No bindings yet
        }
    }
}