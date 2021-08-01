using System;
using System.Collections.Generic;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;
using Xamarin.Forms;

namespace Plapp.Views.Infrastructure
{
    public class ViewLayoutBuilder : IViewLayoutBuilder
    {
        private readonly Dictionary<Type, Type> _pageMap = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _popupMap = new Dictionary<Type, Type>();
        
        public ViewLayoutBuilder BindPage<TViewModel, TView>()
            where TViewModel : IIOViewModel
            where TView : BaseContentPage<TViewModel>
        {
            _pageMap[typeof(TViewModel)] = typeof(TView);

            return this;
        }

        public ViewLayoutBuilder BindPopup<TViewModel, TView>()
            where TViewModel : ITaskViewModel, IIOViewModel
            where TView : BasePopupPage<TViewModel>
        {
            _popupMap[typeof(TViewModel)] = typeof(TView);

            return this;
        }

        public Dictionary<Type, Type> BuildPages() => _pageMap;
        public Dictionary<Type, Type> BuildPopups() => _popupMap;
    }
}