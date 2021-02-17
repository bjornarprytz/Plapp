using System;
using Xamarin.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;

namespace Plapp
{
    public class MainPage : BaseContentPage<IApplicationViewModel>
    {
        public MainPage()
        {
            Content = new Grid
            {
                Children =
                {
                    new CollectionView()
                        .ItemsLayout(new GridItemsLayout(2, ItemsLayoutOrientation.Vertical))
                        .ItemSizingStrategy(ItemSizingStrategy.MeasureAllItems)
                        .VerticalOptions(LayoutOptions.StartAndExpand)
                        .BindItems(nameof(VM.Topics), new DataTemplate(() => new TopicThumbnail())),

                    ViewHelpers.FloatingActionButton()
                        .HorizontalOptions(LayoutOptions.EndAndExpand)
                        .VerticalOptions(LayoutOptions.EndAndExpand)
                        .BindCommand(nameof(VM.AddTopicCommand)),
                }
            };
        }

    }
}

