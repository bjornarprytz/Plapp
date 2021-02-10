using Plapp.Core;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

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

        public Page CreateView<TViewModel>() 
            where TViewModel : IViewModel
        {
            return ResolveView<TViewModel>();
        }

        public Page CreateView<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : IViewModel
        {
            var view = ResolveView<TViewModel>();

            setStateAction?.Invoke(view.ViewModel);

            return view;
        }

        public Page CreateView<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            var view = ResolveView<TViewModel>();

            view.ViewModel = viewModel;

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
