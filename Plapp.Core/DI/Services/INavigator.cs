using System;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface INavigator
    {
        Task<IRootViewModel> GoBackAsync();

        Task BackToRootAsync();

        Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IRootViewModel;
        
        Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IRootViewModel;
    }
}
