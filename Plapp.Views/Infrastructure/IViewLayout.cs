using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public interface IViewLayout
    {
        Type ResolvePage<TViewModel>()
            where TViewModel : class, IViewModel;
        
        Type ResolvePopup<TViewModel>()
            where TViewModel : class, ITaskViewModel;

        IViewLayout BindPage<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : BaseContentPage<TViewModel>;

        IViewLayout BindPopup<TViewModel, TView>()
            where TViewModel : class, ITaskViewModel
            where TView : BasePopupPage<TViewModel>;
    }
}