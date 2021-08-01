using Lottie.Forms;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using Plapp.Views.Extensions;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.Views.Pages
{
    public class LoadingPage : BaseContentPage<ILoadingViewModel>
    {
        AnimationView animation;

        public LoadingPage(ILoadingViewModel vm, ILogger logger) : base(vm, logger)
        {
            animation = new AnimationView
            {
                AutoPlay = true,
                RepeatMode = RepeatMode.Infinite
            }.Bind(AnimationView.AnimationProperty, nameof(VM.Animation));

            Content = new Grid
            {
                Children =
                {
                    animation
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await animation.SillyAnimation();
        }
    }
}
