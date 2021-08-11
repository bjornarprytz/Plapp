using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public interface IViewLayout
    {
        Type ResolvePage<TViewModel>()
            where TViewModel : IViewModel;
        
        Type ResolvePopup<TViewModel>()
            where TViewModel : ITaskViewModel;

        IViewLayout BindPage<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>;

        IViewLayout BindPopup<TViewModel, TView>()
            where TViewModel : ITaskViewModel
            where TView : BasePopupPage<TViewModel>;
    }
}