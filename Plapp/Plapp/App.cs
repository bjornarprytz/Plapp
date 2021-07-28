using Dna;
using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using Plapp.Persist;
using Plapp.ViewModels;
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
                .AddBusinessLogic()
                .AddDataServices()
                .AddDbContext()
                .AddViewModels()
                .AddDataMapper()
                .AddCamera()
                .AddNavigation()
                .AddPrompter()
                .Build();

            var loadingPage = Framework.Provider.Get<IViewFactory>().CreatePage<ILoadingViewModel>();

            MainPage = new NavigationPage(loadingPage);
        }

        protected override async void OnStart()
        {
            ResetDb();

            EnsureDbCreated();

            await Framework.Provider.Get<INavigator>().GoToAsync<IApplicationViewModel>();
        }

        private void ResetDb()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "Plapp.db");

            if (!File.Exists(path))
                return;

            var contextFactory = Framework.Provider.Get<IDbContextFactory<PlappDbContext>>();

            using var context = contextFactory.CreateDbContext();

            context.Database.EnsureDeleted();
        }

        private void EnsureDbCreated()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "Plapp.db");

            if (!File.Exists(path))
                File.Create(path);

            var contextFactory = Framework.Provider.Get<IDbContextFactory<PlappDbContext>>();

            using var context = contextFactory.CreateDbContext();

            context.Database.EnsureCreated();
        }
    }
}
