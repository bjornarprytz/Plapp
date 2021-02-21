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
                        new Entry()
                            .Bind(nameof(VM.Title)),

                        ViewHelpers.PhotoFrame(
                                nameof(VM.ImageUri),
                                nameof(VM.LacksImage),
                                nameof(VM.AddImageCommand)),

                        new Editor()
                            .AutoSize(EditorAutoSizeOption.TextChanges)
                            .Bind(nameof(VM.Description)),

                        new CollectionView()
                            .BindItems(nameof(VM.DataSeries), new DataTemplate(() => new DataSeriesInfoCard())),
                                
                        new Button()
                            .BindCommand(nameof(VM.AddDataSeriesCommand)),
                    }
                },
            };
        }
    }
}
