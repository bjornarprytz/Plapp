using Lottie.Forms;
using Plapp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Markup;

namespace Plapp
{
    class LoadingPage : BaseContentPage<LoadingViewModel>
    {
        AnimationView animation;

        public LoadingPage()
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
