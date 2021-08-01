using System;
using Plapp.Core;

namespace Plapp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TViewModel Create<TViewModel>(Action<TViewModel> setStateAction) where TViewModel : IViewModel
        {
            var vm = Create<TViewModel>();

            setStateAction?.Invoke(vm);

            return vm;
        }

        public TViewModel Create<TViewModel>() where TViewModel : IViewModel
        {
            return _serviceProvider.Get<TViewModel>();
        }
    }
}