using Dna;
using Microsoft.EntityFrameworkCore;
using Plapp.Persist;
using Plapp.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Configuration;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Plapp
{
    public class App : Application
    {
        public App()
        {
            var config = ReadConfig();
            
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddConfiguration(config)
                .AddModules()
                .Build();

            MainPage = new AppShell();
            
        }

        protected override async void OnStart()
        {
            //ResetDb(); // Uncomment if the DB needs to be cleaned up for testing purposes!

            EnsureDbCreated();

            var page = Shell.Current;
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

        private IConfigurationRoot ReadConfig()
        {
            var configStream = FileSystem.OpenAppPackageFileAsync("appsettings.json")
                .GetAwaiter()
                .GetResult();

            var configRoot = new ConfigurationBuilder()
                .AddJsonStream(configStream)
                .Build();

            return configRoot;
        }
    }
}
