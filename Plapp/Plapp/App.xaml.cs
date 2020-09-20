using Nancy.TinyIoc;
using Xamarin.Forms;

namespace Plapp
{
    public partial class App : Application
    {
        public IViewFactory ViewFactory { get; private set; }
        public App()
        {
            TinyIoCContainer.Current.BuildUp(this);

            ServiceLocator.Setup();
            ViewModelLocator.Setup();
            ViewLocator.Setup();

            InitializeComponent();

            MainPage = ViewFactory.Resolve<IDiaryViewModel>();

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
