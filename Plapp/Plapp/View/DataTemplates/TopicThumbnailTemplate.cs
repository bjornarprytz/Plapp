using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicThumbnailTemplate : BaseDataTemplateSelector<ITopicViewModel>
    {
        protected override DataTemplate OnSelectTypedTemplate(ITopicViewModel viewModel, BindableObject container)
        {
            return new DataTemplate(() =>
            {
                return new ContentView
                {

                    Content = new Frame
                    {
                        CornerRadius = 10,
                        Padding = 5,

                        Content = new StackLayout
                        {
                            GestureRecognizers =
                            {
                                new TapGestureRecognizer().BindCommand(nameof(viewModel.OpenTopicCommand))
                            },

                            Children =
                            {
                                new Image()
                                    .Aspect(Aspect.AspectFit)
                                    .Bind(nameof(viewModel.ImageUri)),

                                new StackLayout
                                {
                                    Children =
                                    {
                                        new Label()
                                            .Bind(nameof(viewModel.Title)),
                                        new Label()
                                            .LineBreakMode(LineBreakMode.TailTruncation)
                                            .TextColor(Palette.White)
                                            .Bind(nameof(viewModel.Description))
                                    }
                                }
                            }
                        }
                    }
                    .BackgroundColor(Palette.BackgroundLight)
                    .BorderColor(Palette.Border)
                };
            });
        }
    }
}
