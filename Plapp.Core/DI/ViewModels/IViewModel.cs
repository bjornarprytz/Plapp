using System.ComponentModel;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsShowing { get; }
        Task AppearingAsync();
        Task DisappearingAsync();
    }
}
