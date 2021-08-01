using Plapp.Core;
using Plapp.Views.Pages;
using Plapp.Views.Popups;

namespace Plapp.Views.Infrastructure
{
    public interface IViewLayout
    {
        BaseContentPage<TViewModel> ResolvePage<TViewModel>()
            where TViewModel : IIOViewModel;
        
        BasePopupPage<TViewModel> ResolvePopup<TViewModel>()
            where TViewModel : ITaskViewModel, IIOViewModel;
    }
}