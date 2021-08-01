using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public class ViewFactory : IViewFactory
    {
        private readonly IViewLayout _viewLayout;

        public ViewFactory(IViewLayout viewLayout)
        {
            _viewLayout = viewLayout;
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>() 
            where TViewModel : IIOViewModel
        {
            return _viewLayout.ResolvePage<TViewModel>();
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : IIOViewModel
        {
            var view = _viewLayout.ResolvePage<TViewModel>();

            setStateAction?.Invoke(view.VM);

            return view;
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>(TViewModel viewModel)
            where TViewModel : IIOViewModel
        {
            var view = _viewLayout.ResolvePage<TViewModel>();

            view.VM = viewModel;

            return view;
        }
        public BasePopupPage<TViewModel> CreatePopup<TViewModel>() 
            where TViewModel : ITaskViewModel, IIOViewModel
        {
            return _viewLayout.ResolvePopup<TViewModel>();
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : ITaskViewModel, IIOViewModel
        {
            var view = _viewLayout.ResolvePopup<TViewModel>();

            setStateAction?.Invoke(view.VM);

            return view;
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>(TViewModel viewModel) 
            where TViewModel : ITaskViewModel, IIOViewModel
        {
            var view = _viewLayout.ResolvePopup<TViewModel>();

            view.VM = viewModel;

            return view;
        }
    }
}
