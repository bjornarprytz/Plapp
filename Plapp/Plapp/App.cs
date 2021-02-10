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
        }

        protected override async void OnStart()
        {
            var cts = new CancellationTokenSource();

            await FileHelpers.EnsureDbCreatedAsync(cts.Token);

            await IoC.Get<IPlappDataStore>().EnsureStorageReadyAsync(cts.Token);

            IoC.Get<IApplicationViewModel>().LoadTopicsCommand.Execute(null);

            MainPage = new NavigationPage(IoC.Get<MainPage>());
        }
    }
}
