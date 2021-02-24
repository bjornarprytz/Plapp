using Plapp.Core;
using System;
using System.Collections.Generic;

namespace Plapp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private Dictionary<Type, Func<IViewModel>> FactoryMap => new Dictionary<Type, Func<IViewModel>>
        {
            {typeof(ITagViewModel), () => new TagViewModel(_serviceProvider)},
        };

        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TViewModel Create<TViewModel>() where TViewModel : IViewModel
        {
            if (FactoryMap.ContainsKey(typeof(TViewModel)))
            {
                return (TViewModel) FactoryMap[typeof(TViewModel)].Invoke();
            }

            return _serviceProvider.Get<TViewModel>();
        }
    }
}
