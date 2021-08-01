using Plapp.Core;
using Plapp.Views.Components;
using Plapp.Views.Extensions.UI;
using Plapp.Views.Helpers;
using Xamarin.Forms;

namespace Plapp.Views.Pages
{
    public class TopicPage : BaseContentPage<ITopicViewModel>
    {
        public TopicPage(ITopicViewModel vm, ILogger logger) : base(vm, logger)
        {
            var descriptionExpander = ViewHelpers.ExpanderWithHeader(new Label { Text = "Description" });
            descriptionExpander.Content = new Editor()
                                .FillExpandVertical()
                                .AutoSize(EditorAutoSizeOption.TextChanges)
                                .Bind(nameof(VM.Description));
              
            Content = new Grid
            {
                Children =
                {
                    new StackLayout
                        {
                            Spacing = 10,
                            Margin = 20,
                            Orientation = StackOrientation.Vertical,

                            Children =
                            {
                                new Entry()
                                    .Bind(nameof(VM.Title)),

                                ViewHelpers.PhotoFrame(
                                        nameof(VM.ImageUri),
                                        nameof(VM.LacksImage),
                                        nameof(VM.AddImageCommand)),

                                descriptionExpander,

                                new CollectionView().ItemsLayout(new GridItemsLayout(ItemsLayoutOrientation.Vertical))
                                    .ItemSizingStrategy(ItemSizingStrategy.MeasureAllItems)
                                    .VerticalOptions(LayoutOptions.StartAndExpand)
                                    .BindItems(nameof(VM.DataSeries), new DataTemplate(() => new DataSeriesInfoCard())),

                            }
                        },
                    ViewHelpers.FloatingActionButton()
                        .MaterialIcon(MaterialIcon.Add)
                        .HorizontalOptions(LayoutOptions.EndAndExpand)
                        .VerticalOptions(LayoutOptions.EndAndExpand)
                        .BindCommand(nameof(VM.AddDataSeriesCommand)),
                }
            };
        }
    }
}
