using Plapp.Core;
using System;

namespace Plapp
{
    public interface IViewFactory
    {
        void BindPage<TViewModel, TView>()
            where TViewModel : IIOViewModel
            where TView : BaseContentPage<TViewModel>;

        void BindPopup<TViewModel, TView>()
            where TViewModel : ITaskViewModel, IIOViewModel
            where TView : BasePopupPage<TViewModel>;

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
