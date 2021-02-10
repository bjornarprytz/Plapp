using Xamarin.Forms;

namespace Plapp
{
    class LoadingPage : BaseContentPage<LoadingViewModel>
    {
        public LoadingPage()
        {
            Content = new Grid
            {
                Children =
                {
                    new Label
                    { 
                        Text  = "Loading"
                    }
                }
            };
        }
    }
}
