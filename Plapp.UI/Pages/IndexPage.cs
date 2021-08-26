using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.ContentViews;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : BaseContentPage<IApplicationViewModel>
    {
        private readonly Button _newTopicButton = new() {Text = "New Topic"};
        private readonly CollectionView _topics = new();
        public IndexPage()
        {
            _topics.ItemTemplate = new DataTemplate(() =>  new TopicThumbnail());

            Content = new StackLayout
            {
                Children =
                {
                    _newTopicButton,
                    _topics
                }
            };
        }
        
        

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, vm => vm.Topics, page => page._topics.ItemsSource)
                .DisposeWith(bindingsDisposable);

            this.BindCommand(ViewModel, vm => vm.AddTopicCommand, page => page._newTopicButton);
        }
    }
}