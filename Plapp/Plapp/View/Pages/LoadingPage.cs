using Lottie.Forms;
using Plapp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Markup;
using Microsoft.Extensions.Logging;
using Plapp.Core;

namespace Plapp
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
