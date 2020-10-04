using Dna;
using Plapp.Core;
using Plapp.Relational;
using Xamarin.Forms;

namespace Plapp
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();
        }

        protected override async void OnStart()
        {
            Framework.Construct<DefaultFrameworkConstruction>()
                .UsePlappDatabase()
                .AddViewModels()
                .AddNavigation()
                .Build();

            MainPage = new NavigationPage(
                IoC.Get<IViewFactory>()
                 .CreateView<IDiaryViewModel>());

            var dataStore = IoC.Get<IPlappDataStore>();

           // await dataStore.EnsureDbCreatedAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
