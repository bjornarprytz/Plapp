using Plapp.Core;
using System;

namespace Plapp
{
    public interface IViewFactory
    {
        void BindPage<TViewModel, TView>()
            where TViewModel : IRootViewModel
            where TView : BaseContentPage<TViewModel>;

        void BindPopup<TViewModel, TView>()
            where TViewModel : ITaskViewModel, IRootViewModel
            where TView : BasePopupPage<TViewModel>;

        BaseContentPage<TViewModel> CreatePage<TViewModel>() 
            where TViewModel : IRootViewModel;

        BaseContentPage<TViewModel> CreatePage<TViewModel>(TViewModel viewModel)
            where TViewModel : IRootViewModel;

        BaseContentPage<TViewModel> CreatePage<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : IRootViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>()
            where TViewModel : ITaskViewModel, IRootViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>(TViewModel viewModel)
            where TViewModel : ITaskViewModel, IRootViewModel;

        BasePopupPage<TViewModel> CreatePopup<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : ITaskViewModel, IRootViewModel;
    }
}
