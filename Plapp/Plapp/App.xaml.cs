using Xamarin.Forms;

namespace Plapp
{
    public partial class App : Application
    {
        public App()
        {
            ServiceLocator.Setup();
            ViewModelLocator.Setup();

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
