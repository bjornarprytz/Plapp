using Dna;
using Plapp.Core;
using System.Threading;
using Xamarin.Forms;

namespace Plapp
{
    public class App : Application
    {
        public App(IConfigurationStreamProviderFactory configStreamProviderFactory)
        {
            Resources = Styles.Implicit;
            
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileSystem()
                .AddConfig(configStreamProviderFactory)
                .AddDefaultLogger()
                .AddPlappDataStore()
                .AddViewModels()
                .AddCamera()
                .AddNavigation()
                .Build();

            MainPage = new NavigationPage(new LoadingPage()); // TODO: Use IoC.Get<ILoadingViewModel>() here and implement ILoadingViewModel
        }

        protected override async void OnStart()
        {
            var cts = new CancellationTokenSource();

            await FileHelpers.EnsureDbCreatedAsync(cts.Token);

            await IoC.Get<IPlappDataStore>().EnsureStorageReadyAsync(cts.Token);

            await NavigationHelpers.NavigateTo<IApplicationViewModel>();
        }
    }
}
