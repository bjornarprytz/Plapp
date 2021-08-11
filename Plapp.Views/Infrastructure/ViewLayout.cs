using System;
using System.Collections.Generic;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public class ViewLayout : IViewLayout
    {
        private readonly Dictionary<Type, Type> _pageMap = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _popupMap = new Dictionary<Type, Type>();

        public IViewLayout BindPage<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : BaseContentPage<TViewModel>
        {
            _pageMap[typeof(TViewModel)] = typeof(TView);

            return this;
        }

        public IViewLayout BindPopup<TViewModel, TView>()
            where TViewModel : class, ITaskViewModel
            where TView : BasePopupPage<TViewModel>
        {
            _popupMap[typeof(TViewModel)] = typeof(TView);

            return this;
        }
        
        public Type ResolvePage<TViewModel>()
            where TViewModel : class, IViewModel
        {
            var viewModelType = typeof(TViewModel);

            if (!_pageMap.ContainsKey(viewModelType))
            {
                throw new ArgumentException($"View model(type: {viewModelType}) not bound to a Page. Remember to bind it first :)");
            }

            return _pageMap[viewModelType];
        }

        public Type ResolvePopup<TViewModel>()
            where TViewModel : class, ITaskViewModel
        {
            var viewModelType = typeof(TViewModel);

            if (!_popupMap.ContainsKey(viewModelType))
            {
                throw new ArgumentException($"View model(type: {viewModelType}) not bound to a Popup. Remember to bind it first :)");
            }

            return _popupMap[viewModelType];
        }
    }
}