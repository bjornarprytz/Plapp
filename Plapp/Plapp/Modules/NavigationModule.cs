using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.DependencyInjection;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Plapp.Modules
{
    public class NavigationModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<TopicPage>();
            
            services.AddTransient<LoadingPage>();
            services.AddTransient<DataSeriesPage>();
            services.AddTransient<CreateTagPopup>();
            services.AddTransient<CreateDataPointsPopup>();
            
            services.AddSingleton(PlappViews.Configure());

            services.AddScoped<IPrompter, Prompter>();
            
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton(provider => PopupNavigation.Instance);
            
            services.AddSingleton(provider => Application.Current.MainPage.Navigation);
        }
    }
}