using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.ContentViews
{
    public class TopicThumbnail : BaseContentView<ITopicViewModel>
    {
        private readonly Button _openButton = new ()
        {
            Text = "Hello world"
        };
        
        public TopicThumbnail()
        {
            Content = new StackLayout
            {
                Children =
                {
                    _openButton
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(this.ViewModel, topic => topic.Title, page => page._openButton.Text)
                .DisposeWith(bindingsDisposable);

            this.BindCommand(this.ViewModel, topic => topic.OpenCommand, v => v._openButton)
                .DisposeWith(bindingsDisposable);
        }
    }
}