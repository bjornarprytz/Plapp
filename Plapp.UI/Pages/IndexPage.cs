using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.ContentViews;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : BaseContentPage<IApplicationViewModel>
    {
        private readonly Button _addTopicButton = new()
        {
            Text = "Add Topic",
            HorizontalOptions = LayoutOptions.EndAndExpand,
            VerticalOptions = LayoutOptions.EndAndExpand,
            HeightRequest = 80,
            WidthRequest = 80,
            CornerRadius = 40,
        };
        private readonly CollectionView _topics = new()
        {
            ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical),
            ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
            VerticalOptions = LayoutOptions.StartAndExpand
        };
        
        public IndexPage()
        {
            _topics.ItemTemplate = new DataTemplate(() =>  new TopicThumbnail());

            Content = new Grid
            {
                Children =
                {
                    _topics,
                    _addTopicButton,
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, vm => vm.Topics, page => page._topics.ItemsSource)
                .DisposeWith(bindingsDisposable);

            this.BindCommand(ViewModel, vm => vm.AddTopicCommand, page => page._addTopicButton)
                .DisposeWith(bindingsDisposable);
        }
    }
}