using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicPage : BaseContentPage<ITopicViewModel>
    {
        public TopicPage()
        {
            var descriptionExpander = ViewHelpers.ExpanderWithHeader(new Label { Text = "Description" });
            descriptionExpander.Content = new Editor()
                                .FillExpandVertical()
                                .AutoSize(EditorAutoSizeOption.TextChanges)
                                .Bind(nameof(VM.Description));


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

                        descriptionExpander,

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
