using Plapp.Core;
using System;

namespace Plapp
{
    public interface IViewFactory
    {
        void Bind<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>;

        BaseContentPage<TViewModel> CreateView<TViewModel>() 
            where TViewModel : IViewModel;

        BaseContentPage<TViewModel> CreateView<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        BaseContentPage<TViewModel> CreateView<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : IViewModel;
    }
}
