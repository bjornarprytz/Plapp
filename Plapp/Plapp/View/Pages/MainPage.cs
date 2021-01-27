using System;
using Xamarin.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;

namespace Plapp
{
    public class MainPage : BaseContentPage<ApplicationViewModel>
    {
        public MainPage()
        {
            ViewModel = (ApplicationViewModel)IoC.Get<IApplicationViewModel>();

            Content = new Grid
            {


                Children =
                {
                    new CollectionView
                    {
                        VerticalOptions = LayoutOptions.StartAndExpand
                    }
                    .DataTemplate()
                    .BindItems(nameof(ViewModel.Topics)),

                    new Button
                    {
                        Text = "Hello world, add some topics"
                        , VerticalOptions = LayoutOptions.End

                    }.BindCommand(nameof(ViewModel.AddTopicCommand))
                }
            };
        }

    }
        public static class Extensions
        {
            public static ItemsView BindItems(this ItemsView collectionView, string name)
            {
                collectionView.SetBinding(ItemsView.ItemsSourceProperty, name);

                return collectionView;
            }

            public static ItemsView DataTemplate(this ItemsView collectionView)
            {
                collectionView.ItemTemplate = new BaseDataTemplateSelector<TopicViewModel>
                {
                    ValidTemplate = new DataTemplate(() =>
                    {
                        return new ContentView
                        {
                            Content = new Grid
                            {
                                ColumnDefinitions =
                                {
                                    new ColumnDefinition { Width = GridLength.Auto },
                                    new ColumnDefinition { Width = GridLength.Star },
                                    new ColumnDefinition { Width = GridLength.Star },
                                },

                                GestureRecognizers = 
                                {
                                    new TapGestureRecognizer().BindCommand("OpenTopicCommand") // TODO: Make ViewModel specific with nameof(...)
                                },

                                Children =
                                {
                                    new Image().Column(0).Bind("ImageUri"), // TODO: Make ViewModel specific with nameof(...)
                                    new StackLayout
                                    {
                                        Children =
                                        {
                                            new Label().Bind("Title"),  // TODO: Make ViewModel specific with nameof(...)
                                            new Label{ LineBreakMode = LineBreakMode.TailTruncation }.Bind("Description")
                                        }
                                    }.Column(1)
                                }
                            }
                        };
                        
                    })
                };

                return collectionView;
            }
        }
    }

