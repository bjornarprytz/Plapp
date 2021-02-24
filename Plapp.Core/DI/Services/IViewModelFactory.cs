

using System;

namespace Plapp.Core
{
    public interface IViewModelFactory
    {
        TViewModel Create<TViewModel>(Action<TViewModel> setStateAction=null) where TViewModel : IViewModel;
    }
}
