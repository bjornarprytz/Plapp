using System;
using Xamarin.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using MaterialDesign.Icons;

namespace Plapp
{
    public class MainPage : BaseContentPage<IApplicationViewModel>
    {
        public MainPage(IApplicationViewModel vm) : base(vm)
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
                        .MaterialIcon(MaterialIcon.Add)
                        .HorizontalOptions(LayoutOptions.EndAndExpand)
                        .VerticalOptions(LayoutOptions.EndAndExpand)
                        .BindCommand(nameof(VM.AddTopicCommand)),
                }
            };
        }

    }
}

