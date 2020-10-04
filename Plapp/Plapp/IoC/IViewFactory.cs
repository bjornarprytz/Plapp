using Plapp.Core;
using System;
using Xamarin.Forms;

namespace Plapp
{
    public interface IViewFactory
    {
        void Bind<TViewModel, TView>() 
            where TViewModel : class, IViewModel
            where TView : Page;

        Page CreateView<TViewModel>() 
            where TViewModel : class, IViewModel;

        Page CreateView<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;

        Page CreateView<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : class, IViewModel;

        Page CreateView<TViewModel>(TViewModel viewModel, Action<TViewModel> setStateAction)
            where TViewModel : class, IViewModel;

        Page CreateView(Type viewModelType);
    }
}
