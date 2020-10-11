using Dna;
using Plapp.Core;
using Plapp.Persist;
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
                .LoadConfiguration()
                .UsePlappDataStore()
                .AddViewModels()
                .AddNavigation()
                .Build();

            MainPage = new NavigationPage(
                IoC.Get<IViewFactory>()
                 .CreateView<IDiaryViewModel>());

            var dataStore = IoC.Get<IPlappDataStore>();

            await dataStore.EnsureStorageReadyAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
