using Plapp.Core;
using System;
using System.Collections.Generic;

namespace Plapp
{
    public class ViewFactory : IViewFactory
    {
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public void Bind<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>
        {
            _map[typeof(TViewModel)] = typeof(TView);
        }

        public BaseContentPage<TViewModel> CreateView<TViewModel>() 
            where TViewModel : IViewModel
        {
            return ResolveView<TViewModel>();
        }

        public BaseContentPage<TViewModel> CreateView<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : IViewModel
        {
            var view = ResolveView<TViewModel>();

            setStateAction?.Invoke(view.VM);

            return view;
        }

        public BaseContentPage<TViewModel> CreateView<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            var view = ResolveView<TViewModel>();

            view.VM = viewModel;

            return view;
        }

        private BaseContentPage<TViewModel> ResolveView<TViewModel>()
            where TViewModel : IViewModel
        {
            var viewModelType = typeof(TViewModel);

            if (!_map.ContainsKey(viewModelType))
            {
                throw new ArgumentException($"Unbound view model type: {viewModelType}. Remember to bind it first :)");
            }

            var view = IoC.Resolve<BaseContentPage<TViewModel>>(_map[viewModelType]);

            return view;
        }
    }
}
