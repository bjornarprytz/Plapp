using System;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface INavigator
    {
        Task<IViewModel> PopAsync();

        Task<IViewModel> PopModalAsync();

        Task PopToRootAsync();

        Task PushAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IViewModel;
        
        Task PushAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        Task PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IViewModel;

        Task PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;
    }
}
