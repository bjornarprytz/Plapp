﻿using Plapp.Core;
using System;
using System.Collections.Generic;

namespace Plapp
{
    public class ViewFactory : IViewFactory
    {
        private readonly Dictionary<Type, Type> _pageMap = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _popupMap = new Dictionary<Type, Type>();

        public void BindPage<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>
        {
            _pageMap[typeof(TViewModel)] = typeof(TView);
        }

        public void BindPopup<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BasePopupPage<TViewModel>
        {
            _popupMap[typeof(TViewModel)] = typeof(TView);
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>() 
            where TViewModel : IViewModel
        {
            return ResolvePage<TViewModel>();
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>(Action<TViewModel> setStateAction) 
            where TViewModel : IViewModel
        {
            var view = ResolvePage<TViewModel>();

            setStateAction?.Invoke(view.VM);

            return view;
        }

        public BaseContentPage<TViewModel> CreatePage<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            var view = ResolvePage<TViewModel>();

            view.VM = viewModel;

            return view;
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>() where TViewModel : IViewModel
        {
            return ResolvePopup<TViewModel>();
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>(Action<TViewModel> setStateAction) where TViewModel : IViewModel
        {
            var view = ResolvePopup<TViewModel>();

            setStateAction?.Invoke(view.VM);

            return view;
        }

        public BasePopupPage<TViewModel> CreatePopup<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            var view = ResolvePopup<TViewModel>();

            view.VM = viewModel;

            return view;
        }

        private BaseContentPage<TViewModel> ResolvePage<TViewModel>()
            where TViewModel : IViewModel
        {
            var viewModelType = typeof(TViewModel);

            if (!_pageMap.ContainsKey(viewModelType))
            {
                throw new ArgumentException($"View model(type: {viewModelType}) not bound to a Page. Remember to bind it first :)");
            }

            var view = IoC.Resolve<BaseContentPage<TViewModel>>(_pageMap[viewModelType]);

            return view;
        }

        private BasePopupPage<TViewModel> ResolvePopup<TViewModel>()
            where TViewModel : IViewModel
        {
            var viewModelType = typeof(TViewModel);

            if (!_popupMap.ContainsKey(viewModelType))
            {
                throw new ArgumentException($"View model(type: {viewModelType}) not bound to a Popup. Remember to bind it first :)");
            }

            var view = IoC.Resolve<BasePopupPage<TViewModel>>(_popupMap[viewModelType]);

            return view;
        }
    }
}
