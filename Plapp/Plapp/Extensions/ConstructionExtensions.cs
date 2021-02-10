using Plapp.Core;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using PCLStorage;
using Plapp.Persist;
using Microsoft.EntityFrameworkCore;
using Plapp.Peripherals;

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

                var connStr = $"Data Source={FileHelpers.PathFromRoot(config.ConnectionStrings.PlappDb)}";
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

        public static FrameworkConstruction AddCamera(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<ICamera>(new Camera());

            return construction;
        }

        private static IViewFactory ChainBind<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>
        {
            viewFactory.Bind<TViewModel, TView>();

            return viewFactory;
        }
    }
}
