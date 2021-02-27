using Plapp.Core;
using System;

namespace Plapp
{
    public interface IViewFactory
    {
        void BindPage<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>;

        void BindPopup<TViewModel, TView>()
            where TViewModel : ITaskViewModel
            where TView : BasePopupPage<TViewModel>;

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
