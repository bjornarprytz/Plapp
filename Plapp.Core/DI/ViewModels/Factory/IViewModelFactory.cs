using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp.Core
{
    public interface IViewModelFactory
    {
        TViewModel Create<TViewModel>(Action<TViewModel> setStateAction) where TViewModel : IViewModel;
        TViewModel Create<TViewModel>() where TViewModel : IViewModel;
    }
}
