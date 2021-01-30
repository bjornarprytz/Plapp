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
                    new CollectionView()
                        .VerticalOptions(LayoutOptions.StartAndExpand)
                        .DataTemplate(new TopicThumbnailTemplate())
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
}

