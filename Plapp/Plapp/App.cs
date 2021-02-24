using Dna;
using PCLStorage;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;
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
                .AddPrompter()
                .Build();

            MainPage = new NavigationPage(new LoadingPage()); // TODO: Use IoC.Get<ILoadingViewModel>() here and implement ILoadingViewModel
        }

        protected override async void OnStart()
        {
            var cts = new CancellationTokenSource();

            await EnsureDbCreatedAsync(cts.Token);

            await IoC.Get<IPlappDataStore>().EnsureStorageReadyAsync(cts.Token);

            await IoC.Get<INavigator>().GoToAsync<IApplicationViewModel>();
        }


        private async Task EnsureDbCreatedAsync(CancellationToken cancellationToken)
        {
            switch (await IoC.Get<IFileSystem>().LocalStorage.CheckExistsAsync("Plapp.db", cancellationToken))
            {
                case ExistenceCheckResult.NotFound:
                    await IoC.Get<IFileSystem>().LocalStorage.CreateFileAsync("Plapp.db", CreationCollisionOption.FailIfExists, cancellationToken);
                    break;
                default:
                    break;
            }
        }
    }
}
