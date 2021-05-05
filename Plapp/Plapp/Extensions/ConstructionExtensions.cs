﻿using Plapp.Core;
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

namespace Plapp
{
    public static class ConstructionExtensions
    {

        public static FrameworkConstruction AddConfig(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IConfigurationManager>(new DefaultConfigurationManager("appsettings.json"));

            return construction;
        }

        public static FrameworkConstruction AddDataServices(this FrameworkConstruction construction)
        {
            construction.Services.AddDbContext<PlappDbContext>(async options =>
            {
                var config = await IoC.Get<IConfigurationManager>().GetAsync();

                var connStr = $"Data Source={Path.Combine(FileSystem.AppDataDirectory, config.ConnectionStrings.PlappDb)}";
                options.UseSqlite(connStr);
            }, contextLifetime: ServiceLifetime.Scoped);

            construction.Services.AddTransient<IDataSeriesService, DataSeriesService>();
            construction.Services.AddTransient<ITagService, TagService>();
            construction.Services.AddTransient<ITopicService, TopicService>();

            return construction;
        }

        public static FrameworkConstruction AddViewModels(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IApplicationViewModel, ApplicationViewModel>();

            construction.Services.AddTransient<ITagViewModel, TagViewModel>();
            construction.Services.AddTransient<ITopicViewModel, TopicViewModel>();
            construction.Services.AddTransient<IDataSeriesViewModel, DataSeriesViewModel>();
            construction.Services.AddTransient<IDataPointViewModel, DataPointViewModel>();
            construction.Services.AddTransient<ICreateViewModel<ITagViewModel>, CreateTagViewModel>();
            construction.Services.AddTransient<ICreateMultipleViewModel<IDataPointViewModel>, CreateDataPointsViewModel>();


            return construction;
        }

        public static FrameworkConstruction AddNavigation(this FrameworkConstruction construction)
        {

            construction.Services.AddSingleton<MainPage>();
            construction.Services.AddSingleton<TopicPage>();

            construction.Services.AddTransient<CreateTagPopup>();
            construction.Services.AddTransient<CreateDataPointsPopup>();

            construction.Services.AddSingleton(provider =>
                new ViewFactory()
                    .ChainBindPage<IApplicationViewModel, MainPage>()
                    .ChainBindPage<ITopicViewModel, TopicPage>()

                    .ChainBindPopup<ICreateViewModel<ITagViewModel>, CreateTagPopup>()
                    .ChainBindPopup<ICreateMultipleViewModel<IDataPointViewModel>, CreateDataPointsPopup>()
            );


            construction.Services.AddSingleton<INavigator>(new Navigator());
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

        private static IViewFactory ChainBindPage<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : IRootViewModel
            where TView : BaseContentPage<TViewModel>
        {
            viewFactory.BindPage<TViewModel, TView>();

            return viewFactory;
        }

        private static IViewFactory ChainBindPopup<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : ITaskViewModel, IRootViewModel
            where TView : BasePopupPage<TViewModel>
        {
            viewFactory.BindPopup<TViewModel, TView>();

            return viewFactory;
        }
    }
}
