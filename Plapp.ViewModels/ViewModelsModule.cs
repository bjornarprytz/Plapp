using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.DependencyInjection;

namespace Plapp.ViewModels
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
            services.AddTransient<ICreateViewModel<ITagViewModel>, CreateTagViewModel>();
            services.AddTransient<ICreateMultipleViewModel<IDataPointViewModel>, CreateDataPointsViewModel>();

            services.AddSingleton<ViewModelFactory<ITagViewModel>>(provider => () => provider.Get<ITagViewModel>());
            services.AddSingleton<ViewModelFactory<ITopicViewModel>>(provider => () => provider.Get<ITopicViewModel>());
            services.AddSingleton<ViewModelFactory<IDataSeriesViewModel>>(provider => () => provider.Get<IDataSeriesViewModel>());
            services.AddSingleton<ViewModelFactory<IDataPointViewModel>>(provider => () => provider.Get<IDataPointViewModel>());
            services.AddSingleton<ViewModelFactory<ICreateViewModel<ITagViewModel>>>(provider => () => provider.Get<ICreateViewModel<ITagViewModel>>());

        }
    }
}