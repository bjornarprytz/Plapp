using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : IViewModel
        where TViewModel : IViewModel
    {
        IViewModel UnderCreation { get; }
        Task<TViewModel> Creation();
    }
}
