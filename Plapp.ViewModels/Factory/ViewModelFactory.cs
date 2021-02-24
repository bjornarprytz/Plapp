using Plapp.Core;
using System;
using System.Collections.Generic;

namespace Plapp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private Dictionary<Type, Func<IViewModel>> _factoryMap => new Dictionary<Type, Func<IViewModel>>
        {
            {typeof(ITagViewModel), () => new TagViewModel(_serviceProvider)},
            {typeof(ITopicViewModel), () => new TopicViewModel(_serviceProvider)},
            {typeof(IDataSeriesViewModel), () => new DataSeriesViewModel(_serviceProvider) },
            {typeof(IDataPointViewModel), () => new DataPointViewModel(_serviceProvider) }
        };

        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TViewModel Create<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : IViewModel
        {
            var vm = _factoryMap.TryGetValue(typeof(TViewModel), out var getter)
                ? (TViewModel)getter.Invoke()
                : throw new ArgumentException($"Cannot create instance of ViewModel of type {typeof(TViewModel)}");

            setStateAction?.Invoke(vm);

            return vm;
        }
    }
}
