using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plapp.Core;
using Plapp.UI.Pages;
using Plapp.ViewModels;

namespace Plapp.DependencyInjection
{
    public class ViewModelsModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IApplicationViewModel, ApplicationViewModel>();

            services.AddTransient<ILoadingViewModel, LoadingViewModel>();

            services.AddTransient<ITagViewModel, TagViewModel>();
            services.AddTransient<ITopicViewModel, TopicViewModel>();
            services.AddTransient<IDataSeriesViewModel, DataSeriesViewModel>();
            services.AddTransient<IDataPointViewModel, DataPointViewModel>();
            services.AddTransient<IEditViewModel<ITagViewModel>, EditTagViewModel>();
            services.AddTransient<IEditViewModel<IDataSeriesViewModel>, EditDataSeriesViewModel>();
            
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        }
    }
}