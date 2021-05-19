using System;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface INavigator
    {
        Task<IIOViewModel> GoBackAsync();

        Task BackToRootAsync();

        Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IIOViewModel;
        
        Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IIOViewModel;
    }
}
