using Dna;
using Plapp.Core;
using Plapp.Peripherals;
using Plapp.Persist;
using Xamarin.Forms;

namespace Plapp.Test
{
    public class App : Application
    {
        public App()
        {
            Resources = Styles.Implicit;
        }

        protected override async void OnStart()
        {
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddDefaultLogger()
                .UsePlappDataStore()
                .AddViewModels()
                .AddIcons()
                .AddCamera()
                .AddNavigation()
                .Build();

            await IoC.Get<IPlappDataStore>().EnsureStorageReadyAsync();

            IoC.Get<IApplicationViewModel>().LoadTopicsCommand.Execute(null);

            MainPage = new NavigationPage(
                IoC.Get<IViewFactory>()
                 .CreateView<IApplicationViewModel>());

        }
    }
}
