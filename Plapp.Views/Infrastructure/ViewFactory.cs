using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public class ViewFactory : IViewFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IViewLayout _viewLayout;

        public ViewFactory(IServiceProvider serviceProvider, Action<IViewLayout> configure)
        {
            _serviceProvider = serviceProvider;
            _viewLayout = new ViewLayout();

            configure(_viewLayout);
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>() 
            where TViewModel : class, IViewModel
        {
            return ResolvePage<TViewModel>();
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : class, IViewModel
        {
            var view = ResolvePage<TViewModel>();

            setStateAction?.Invoke(view.VM);

            return view;
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var view = ResolvePage<TViewModel>();

            view.VM = viewModel;

            return view;
        }
        public BasePopupPage<TViewModel> CreatePopup<TViewModel>() 
            where TViewModel : class, ITaskViewModel
        {
            return ResolvePopup<TViewModel>();
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : class, ITaskViewModel
        {
            var view = ResolvePopup<TViewModel>();

            setStateAction?.Invoke(view.ViewModel);

            return view;
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>(TViewModel viewModel) 
            where TViewModel : class, ITaskViewModel
        {
            var view = ResolvePopup<TViewModel>();

            view.ViewModel = viewModel;

            return view;
        }


        private BasePopupPage<TViewModel> ResolvePopup<TViewModel>()
            where  TViewModel : class, ITaskViewModel
        {
            var popup = _serviceProvider.GetService(_viewLayout.ResolvePopup<TViewModel>()) as BasePopupPage<TViewModel>;
            
            return popup;
        }
        
        private BaseContentPage<TViewModel> ResolvePage<TViewModel>()
            where  TViewModel : class, IViewModel
        {
            var page = _serviceProvider.GetService(_viewLayout.ResolvePage<TViewModel>()) as BaseContentPage<TViewModel>;
            
            return page;
        }
    }
}
