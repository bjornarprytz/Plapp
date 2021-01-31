using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicThumbnailTemplate : BaseDataTemplateSelector<TopicViewModel>
    {
        protected override DataTemplate OnSelectTypedTemplate(TopicViewModel item, BindableObject container)
        {
            return new DataTemplate(() =>
            {
                return new ContentView
                {
                    Content = new Grid
                    {
                        BackgroundColor = Color.Beige,

                        ColumnDefinitions =
                                {
                                    new ColumnDefinition { Width = GridLength.Auto },
                                    new ColumnDefinition { Width = GridLength.Star },
                                    new ColumnDefinition { Width = GridLength.Star },
                                },

                        GestureRecognizers =
                                {
                                    new TapGestureRecognizer().BindCommand(nameof(item.OpenTopicCommand))
                                },

                        Children =
                                {
                                    new Image()
                                    .Column(0)
                                    .HeightRequest(80)
                                    .Aspect(Aspect.AspectFit)
                                    .Bind(nameof(item.ImageUri)),
                                    
                                    new StackLayout
                                    {
                                        Children =
                                        {
                                            new Label()
                                                .Bind(nameof(item.Title)),
                                            new Label()
                                                .LineBreakMode(LineBreakMode.TailTruncation)
                                                .Bind(nameof(item.Description))
                                        }
                                    }.Column(1)
                                }
                    }
                };
            });
        }
    }
}
