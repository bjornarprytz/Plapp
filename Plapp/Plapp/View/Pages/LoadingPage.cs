using Plapp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Plapp
{
    class LoadingPage : BaseContentPage<LoadingViewModel>
    {
        Label label;

        public LoadingPage()
        {
            label = new Label
            {
                Text = "Loading",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Content = new Grid
            {
                Children =
                {
                    label
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            await label.SillyAnimation();
        }
    }
}
