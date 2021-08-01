using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.DependencyInjection;
using Plapp.Views.Infrastructure;
using Plapp.Views.Pages;
using Plapp.Views.Popups;
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

            services.AddSingleton<IViewFactory, ViewFactory>(provider  => new ViewFactory(provider, 
                layout => layout
                        .BindPage<ILoadingViewModel, LoadingPage>()
                        .BindPage<IApplicationViewModel, MainPage>()
                        .BindPage<ITopicViewModel, TopicPage>()
                        .BindPage<IDataSeriesViewModel, DataSeriesPage>()
                        .BindPopup<ICreateViewModel<ITagViewModel>, CreateTagPopup>()
                        .BindPopup<ICreateMultipleViewModel<IDataPointViewModel>, CreateDataPointsPopup>()
                ) );

            services.AddScoped<IPrompter, Prompter>();
            
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton(provider => PopupNavigation.Instance);
            
            services.AddSingleton(provider => Application.Current.MainPage.Navigation);
        }
    }
}