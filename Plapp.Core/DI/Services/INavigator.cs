using System;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface INavigator
    {
        Task<IViewModel> GoBackAsync();

        Task BackToRootAsync();

        Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IViewModel;
        
        Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;
    }
}
