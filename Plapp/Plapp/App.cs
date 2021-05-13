using Dna;
using Plapp.Core;
using System.IO;
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
                .AddDataServices()
                .AddDbContext()
                .AddViewModels()
                .AddCamera()
                .AddNavigation()
                .AddPrompter()
                .Build();

            MainPage = new NavigationPage(new LoadingPage()); // TODO: Use IoC.Get<ILoadingViewModel>() here and implement ILoadingViewModel
        }

        protected override async void OnStart()
        {
            EnsureDbCreated();

            await IoC.Get<INavigator>().GoToAsync<IApplicationViewModel>();
        }


        private void EnsureDbCreated()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "Plapp.db");

            if (!File.Exists(path))
                File.Create(path);
        }
    }
}
