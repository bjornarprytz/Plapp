﻿using System;
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
                                    new TapGestureRecognizer().BindCommand(nameof(viewModel.OpenTopicCommand))
                                },

                        Children =
                                {
                                    new Image()
                                    .Column(0)
                                    .HeightRequest(80)
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
                                                .Bind(nameof(viewModel.Description))
                                        }
                                    }.Column(1)
                                }
                    }
                };
            });
        }
    }
}