using MaterialDesign.Icons;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicPage : BaseContentPage<ITopicViewModel>
    {
        public TopicPage()
        {
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Margin = 20,
                    Orientation = StackOrientation.Vertical,

                    Children =
                    {
                        new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,

                            Children =
                            {
                                new Entry()
                                .Bind(nameof(ViewModel.Title)),

                                new ContentView
                                {
                                    Content = new Grid
                                    {
                                        Children =
                                        {
                                            new Image()
                                                .Bind(nameof(ViewModel.ImageUri)),

                                            new Button()
                                                .MaterialIcon(MaterialIcon.AddAPhoto, IconSize.Huge)
                                                .Bind(IsVisibleProperty, nameof(ViewModel.LacksImage))
                                                .BindCommand(nameof(ViewModel.AddImageCommand))
                                        }
                                    }
                                }
                                    .HeightRequest(300)
                                    .VerticalOptions(LayoutOptions.StartAndExpand)
                                    .HorizontalOptions(LayoutOptions.Fill),

                                new Editor
                                {
                                    AutoSize = EditorAutoSizeOption.TextChanges,
                                }.Bind(nameof(ViewModel.Description))
                            }
                        },

                        new Button()
                            .BindCommand(nameof(ViewModel.AddDataSeriesCommand)),

                        new CollectionView()
                            .BindItems(nameof(ViewModel.DataEntries), new DataSeriesTemplate())
                    }
                }
            };
        }
    }
}
