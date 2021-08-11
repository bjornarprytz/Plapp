using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public interface IViewFactory
    {
        BaseContentPage<TViewModel> CreatePage<TViewModel>() 
            where TViewModel : IViewModel;

        BaseContentPage<TViewModel> CreatePage<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        BaseContentPage<TViewModel> CreatePage<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : IViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>()
            where TViewModel : ITaskViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>(TViewModel viewModel)
            where TViewModel : ITaskViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : ITaskViewModel;
    }
}
