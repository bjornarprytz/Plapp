using System;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface INavigator
    {
        Task<IViewModel> GoBackAsync();

        Task<IViewModel> PopModalAsync();

        Task BackToRootAsync();

        Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IViewModel;
        
        Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        Task PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IViewModel;

        Task PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;
    }
}
