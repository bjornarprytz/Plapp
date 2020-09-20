using Nancy.TinyIoc;
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
            where TView : Page
        {
            _map[typeof(TViewModel)] = typeof(TView);
        }

        public Page CreateView<TViewModel>() where TViewModel : IViewModel
        {
            return CreateView(typeof(TViewModel));
        }

        public Page CreateView<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : class, IViewModel
        {
            var viewModel = IoC.Get<TViewModel>();

            setStateAction?.Invoke(viewModel);

            return CreateView(viewModel);
        }

        public Page CreateView(Type viewModelType)
        {
            if (!_map.ContainsKey(viewModelType))
            {
                throw new ArgumentException($"Unbound view model type: {viewModelType}. Remember to bind it first :)");
            }

            var view = IoC.Resolve<Page>(_map[viewModelType]);

            var viewModel = IoC.Resolve(viewModelType);

            view.BindingContext = viewModel;

            return view;
        }


        private Page CreateView<TViewModel>(TViewModel viewModel)
        {
            var view = IoC.Resolve<Page>(_map[typeof(TViewModel)]);

            view.BindingContext = viewModel;

            return view;
        }
        
    }
}
