

namespace Plapp.Core
{
    public interface IViewModelFactory
    {
        TViewModel Create<TViewModel>() where TViewModel : IViewModel;
    }
}
