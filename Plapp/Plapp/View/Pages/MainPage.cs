using System;
using Xamarin.Forms;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;

namespace Plapp
{
    public class MainPage : BaseContentPage<IApplicationViewModel>
    {
        public MainPage()
        {
            Content = new Grid
            {
                Children =
                {
                    new CollectionView()
                        .VerticalOptions(LayoutOptions.StartAndExpand)
                        .BindItems(nameof(ViewModel.Topics), new TopicThumbnailTemplate()),
                    
                    
                    new Button
                    {
                        Text = "Hello world, add some topics"
                    }
                    .VerticalOptions(LayoutOptions.End)
                    .BindCommand(nameof(ViewModel.AddTopicCommand))
                }
            };
        }

    }
}

