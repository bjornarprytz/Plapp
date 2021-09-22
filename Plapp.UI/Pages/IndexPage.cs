using System.Reactive.Disposables;
using MaterialDesign.Icons;
using Plapp.Core;
using Plapp.UI.ContentViews;
using Plapp.UI.Extensions;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : BaseContentPage<IApplicationViewModel>
    {
        private readonly Button _addTopicButton = new();
        private readonly CollectionView _topics = new();
        
        private readonly Button _addTagButton = new();
        
        public IndexPage()
        {
            Content = new Grid
            {
                Children =
                {
                    _topics
                        .ItemTemplate(() => new TopicSummary())
                        .ItemsLayout(new LinearItemsLayout(ItemsLayoutOrientation.Vertical))
                        .VerticalOptions(LayoutOptions.StartAndExpand),
                    _addTopicButton
                        .MaterialIcon(MaterialIcon.Add)
                        .Circle(80)
                        .VerticalAndHorizontalOptions(LayoutOptions.EndAndExpand),
                    _addTagButton
                        .MaterialIcon(MaterialIcon.TagFaces)
                        .Circle(80)
                        .VerticalAndHorizontalOptions(LayoutOptions.StartAndExpand),
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.OneWayBind(ViewModel, vm => vm.Topics, page => page._topics.ItemsSource)
                .DisposeWith(bindingsDisposable);

            this.BindCommand(ViewModel, vm => vm.AddTopicCommand, page => page._addTopicButton)
                .DisposeWith(bindingsDisposable);
            
            this.BindCommand(ViewModel, vm => vm.AddTagCommand, page => page._addTagButton)
                .DisposeWith(bindingsDisposable);
        }
    }
}