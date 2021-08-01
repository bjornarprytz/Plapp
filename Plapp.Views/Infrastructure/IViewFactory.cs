using System;
using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public interface IViewFactory
    {
        BaseContentPage<TViewModel> CreatePage<TViewModel>() 
            where TViewModel : IIOViewModel;

        BaseContentPage<TViewModel> CreatePage<TViewModel>(TViewModel viewModel)
            where TViewModel : IIOViewModel;

        BaseContentPage<TViewModel> CreatePage<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : IIOViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>()
            where TViewModel : ITaskViewModel, IIOViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>(TViewModel viewModel)
            where TViewModel : ITaskViewModel, IIOViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : ITaskViewModel, IIOViewModel;
    }
}
