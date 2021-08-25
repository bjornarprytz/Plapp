using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.ContentViews
{
    public class TopicThumbnail : BaseViewCell<ITopicViewModel>
    {
        private readonly Label _title = new ()
        {
            Text = "Hello world"
        };
        
        public TopicThumbnail()
        {
            View = new StackLayout
            {
                Children =
                {
                    _title
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            //this.OneWayBind(this.ViewModel, topic => topic.Title, page => page._title.Text);
            
        }
    }
}