﻿
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicThumbnail : BaseContentView<ITopicViewModel>
    {
        public TopicThumbnail()
        {
            Content = new Frame
            {
                CornerRadius = 10,
                Padding = 5,

                Content = new StackLayout
                {
                    GestureRecognizers =
                            {
                                new TapGestureRecognizer().BindCommand(nameof(VM.OpenCommand))
                            },

                    Children =
                            {
                                new Image()
                                    .Aspect(Aspect.AspectFit)
                                    .Bind(nameof(VM.ImageUri)),

                                new StackLayout
                                {
                                    Children =
                                    {
                                        new Label()
                                            .Bind(nameof(VM.Title)),
                                        new Label()
                                            .LineBreakMode(LineBreakMode.TailTruncation)
                                            .TextColor(Palette.White)
                                            .Bind(nameof(VM.Description))
                                    }
                                }
                            }
                }
            }
            .BackgroundColor(Palette.BackgroundLight)
            .BorderColor(Palette.Border);
        }
    }
}
