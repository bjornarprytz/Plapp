using Plapp.Core;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Plapp.Persist;
using Microsoft.EntityFrameworkCore;
using Plapp.Peripherals;
using Plapp.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;

namespace Plapp
{
    public static class ConstructionExtensions
    {

        public static FrameworkConstruction AddConfig(this FrameworkConstruction construction)
        {
            var configStream = FileSystem.OpenAppPackageFileAsync("appsettings.json")
                .GetAwaiter()
                .GetResult();

            var builder = new ConfigurationBuilder()
                .AddJsonStream(configStream)
                .Build();

            construction.AddConfiguration(builder);

            return construction;
        }

        public static FrameworkConstruction AddDataServices(this FrameworkConstruction construction)
        {
            construction.Services.AddTransient<IDataSeriesService, DataSeriesService>();
            construction.Services.AddTransient<ITagService, TagService>();
            construction.Services.AddTransient<ITopicService, TopicService>();

            return construction;
        }

        public static FrameworkConstruction AddDbContext(this FrameworkConstruction construction)
        {
            var dbName = construction.Configuration.GetConnectionString("PlappDb"); // TODO: Clean this up, so that the whole connection string is in the config

            var connStr = $"Data Source={Path.Combine(FileSystem.AppDataDirectory, dbName)}";

            Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connStr);

            construction.Services.AddDbContext<PlappDbContext>(configureDbContext);
            construction.Services.AddSingleton<IDbContextFactory<PlappDbContext>>(new PlappDbContextFactory(configureDbContext));

            return construction;
        }

        public static FrameworkConstruction AddViewModels(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IApplicationViewModel, ApplicationViewModel>();

            construction.Services.AddTransient<LoadingViewModel>(); // TODO: Implement an interface for this?

            construction.Services.AddTransient<ITagViewModel, TagViewModel>();
            construction.Services.AddTransient<ITopicViewModel, TopicViewModel>();
            construction.Services.AddTransient<IDataSeriesViewModel, DataSeriesViewModel>();
            construction.Services.AddTransient<IDataPointViewModel, DataPointViewModel>();
            construction.Services.AddTransient<ICreateViewModel<ITagViewModel>, CreateTagViewModel>();
            construction.Services.AddTransient<ICreateMultipleViewModel<IDataPointViewModel>, CreateDataPointsViewModel>();

            construction.Services.AddSingleton<ViewModelFactory<ITagViewModel>>(provider => () => provider.Get<ITagViewModel>());
            construction.Services.AddSingleton<ViewModelFactory<ITopicViewModel>>(provider => () => provider.Get<ITopicViewModel>());
            construction.Services.AddSingleton<ViewModelFactory<IDataSeriesViewModel>>(provider => () => provider.Get<IDataSeriesViewModel>());
            construction.Services.AddSingleton<ViewModelFactory<IDataPointViewModel>>(provider => () => provider.Get<IDataPointViewModel>());
            construction.Services.AddSingleton<ViewModelFactory<ICreateViewModel<ITagViewModel>>>(provider => () => provider.Get<ICreateViewModel<ITagViewModel>>());


            return construction;
        }

        

        public static FrameworkConstruction AddDataMapper(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton(PlappMapping.Configure(construction.Provider));

            return construction;
        }


        public static FrameworkConstruction AddNavigation(this FrameworkConstruction construction)
        {

            construction.Services.AddSingleton<MainPage>();
            construction.Services.AddSingleton<TopicPage>();

            construction.Services.AddTransient<LoadingPage>();
            construction.Services.AddTransient<CreateTagPopup>();
            construction.Services.AddTransient<CreateDataPointsPopup>();

            construction.Services.AddSingleton(PlappViews.Configure());


            construction.Services.AddSingleton<INavigator, Navigator>();
            construction.Services.AddSingleton(provider => PopupNavigation.Instance);

            construction.Services.AddSingleton(provider => Application.Current.MainPage.Navigation);

            return construction;
        }

        public static FrameworkConstruction AddCamera(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<ICamera>(new Camera());

            return construction;
        }
        
        public static FrameworkConstruction AddPrompter(this FrameworkConstruction construction)
        {
            construction.Services.AddScoped<IPrompter, Prompter>();

            return construction;
        }

        
    }
}
