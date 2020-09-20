using Nancy.TinyIoc;
using Xamarin.Forms;

namespace Plapp
{
    public partial class App : Application
    {
        public IViewFactory ViewFactory => IoC.Get<IViewFactory>();
        public App()
        {
            TinyIoCContainer.Current.BuildUp(this);

            IoC.Setup();

            ViewFactory.Bind<IDiaryViewModel, MainPage>();
            ViewFactory.Bind<ITopicViewModel, TopicPage>();

            InitializeComponent();

            MainPage = new NavigationPage(ViewFactory.CreateView<IDiaryViewModel>());

            TinyIoCContainer.Current.Register(MainPage.Navigation);
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
