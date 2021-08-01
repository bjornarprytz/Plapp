using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public interface IViewLayout
    {
        Type ResolvePage<TViewModel>()
            where TViewModel : IIOViewModel;
        
        Type ResolvePopup<TViewModel>()
            where TViewModel : ITaskViewModel, IIOViewModel;

        IViewLayout BindPage<TViewModel, TView>()
            where TViewModel : IIOViewModel
            where TView : BaseContentPage<TViewModel>;

        IViewLayout BindPopup<TViewModel, TView>()
            where TViewModel : ITaskViewModel, IIOViewModel
            where TView : BasePopupPage<TViewModel>;
    }
}