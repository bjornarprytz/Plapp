using Dna;
using Plapp.Core;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Plapp
{
    public class App : Application
    {
        public App()
        {
            Resources = Styles.Implicit;
            
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddConfig()
                .AddDefaultLogger()
                .AddPlappDataStore()
                .AddViewModels()
                .AddCamera()
                .AddNavigation()
                .AddPrompter()
                .Build();

            MainPage = new NavigationPage(new LoadingPage()); // TODO: Use IoC.Get<ILoadingViewModel>() here and implement ILoadingViewModel
        }

        protected override async void OnStart()
        {
            var cts = new CancellationTokenSource();

            EnsureDbCreatedAsync();

            await IoC.Get<IPlappDataStore>().EnsureStorageReadyAsync(cts.Token);

            await IoC.Get<INavigator>().GoToAsync<IApplicationViewModel>();
        }


        private void EnsureDbCreatedAsync()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "Plapp.db");

            if (!File.Exists(path))
                File.Create(path);
        }
    }
}
