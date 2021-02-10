using Plapp.Core;
using System;
using Xamarin.Forms;

namespace Plapp
{
    public interface IViewFactory
    {
        void Bind<TViewModel, TView>()
            where TViewModel : IViewModel
            where TView : BaseContentPage<TViewModel>;

        Page CreateView<TViewModel>() 
            where TViewModel : IViewModel;

        Page CreateView<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        Page CreateView<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : IViewModel;
    }
}
