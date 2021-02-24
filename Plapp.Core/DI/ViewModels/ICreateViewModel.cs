using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : IViewModel
        where TViewModel : IViewModel
    {
        IViewModel UnderCreation { get; set; }
        Task<TViewModel> Creation();
    }
}
