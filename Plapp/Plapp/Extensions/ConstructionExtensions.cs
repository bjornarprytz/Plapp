using Plapp.Core;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using PCLStorage;
using Plapp.Persist;
using Microsoft.EntityFrameworkCore;
using Plapp.Peripherals;
using Plapp.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Plapp
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction AddFileSystem(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton(provider => FileSystem.Current);

            return construction;
        }

        public static FrameworkConstruction AddConfig(this FrameworkConstruction construction, IConfigurationStreamProviderFactory configStreamProviderFactory)
        {
            construction.Services.AddSingleton<IConfigurationManager>(new ConfigurationManager(configStreamProviderFactory));

            return construction;
        }

        public static FrameworkConstruction AddPlappDataStore(this FrameworkConstruction construction)
        {
            construction.Services.AddDbContext<PlappDbContext>(async options =>
            {
                var config = await IoC.Get<IConfigurationManager>().GetAsync();

                var connStr = $"Data Source={IoC.Get<IFileSystem>().PathFromRoot(config.ConnectionStrings.PlappDb)}";
                options.UseSqlite(connStr);
            }, contextLifetime: ServiceLifetime.Transient);

            construction.Services.AddTransient<IPlappDataStore>(
                provider => new PlappDataStore(
                    () => provider.GetService<PlappDbContext>()));

            return construction;
        }

        public static FrameworkConstruction AddViewModels(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IApplicationViewModel, ApplicationViewModel>();

            construction.Services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            return construction;
        }

        public static FrameworkConstruction AddNavigation(this FrameworkConstruction construction)
        {

            construction.Services.AddSingleton<MainPage>();
            construction.Services.AddSingleton<TopicPage>();

            construction.Services.AddSingleton(provider =>
                new ViewFactory()
                    .ChainBind<IApplicationViewModel, MainPage>()
                    .ChainBind<ITopicViewModel, TopicPage>()
            );


            construction.Services.AddSingleton<INavigator>(new Navigator());

            construction.Services.AddSingleton(provider => Application.Current.MainPage.Navigation);

            return construction;
        }
        
        public static FrameworkConstruction AddPopupNavigation(this FrameworkConstruction construction)
        {
            // TODO: Add popup singletons (?)

            // TODO: Bind viewmodel to popup

            construction.Services.AddSingleton(provider => PopupNavigation.Instance);

            return construction;
        }

        public static FrameworkConstruction AddCamera(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<ICamera>(new Camera());

            return construction;
        }
        
        public static FrameworkConstruction AddPrompter(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IPrompter, Prompter>();

            return construction;
        }

        private static IViewFactory ChainBind<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>
        {
            viewFactory.BindPage<TViewModel, TView>();

            return viewFactory;
        }
    }
}
