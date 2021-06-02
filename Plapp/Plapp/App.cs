using Dna;
using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using Plapp.Persist;
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
                .AddDataMapper()
                .AddCamera()
                .AddNavigation()
                .AddPrompter()
                .Build();

            MainPage = new NavigationPage(IoC.Get<LoadingPage>()); // TODO: Use IoC.Get<ILoadingViewModel>() here and implement ILoadingViewModel
        }

        protected override async void OnStart()
        {
            ResetDb();

            EnsureDbCreated();

            await IoC.Get<IApplicationViewModel>().LoadDataCommand.ExecuteAsync();

            await IoC.Get<INavigator>().GoToAsync<IApplicationViewModel>();
        }

        private void ResetDb()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "Plapp.db");

            if (!File.Exists(path))
                return;

            var contextFactory = IoC.Get<IDbContextFactory<PlappDbContext>>();

            using var context = contextFactory.CreateDbContext();

            context.Database.EnsureDeleted();
        }

        private void EnsureDbCreated()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "Plapp.db");

            if (!File.Exists(path))
                File.Create(path);

            var contextFactory = IoC.Get<IDbContextFactory<PlappDbContext>>();

            using var context = contextFactory.CreateDbContext();

            context.Database.EnsureCreated();
        }
    }
}
