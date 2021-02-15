using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicThumbnailTemplate : BaseDataTemplateSelector<TopicViewModel>
    {
        protected override DataTemplate OnSelectTypedTemplate(TopicViewModel viewModel, BindableObject container)
        {
            return new DataTemplate(() =>
            {
                return new ContentView
                {

                    Content = new Frame
                    {
                        CornerRadius = 10,
                        Padding = 5,
                        BorderColor = Color.White,
                        BackgroundColor = Color.Transparent,

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
                                            .TextColor(Colors.BackgroundVeryLight)
                                            .Bind(nameof(viewModel.Description))
                                    }
                                }
                            }
                        }
                    }
                };
            });
        }
    }
}
