using System.ComponentModel;
using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IViewModel : INotifyPropertyChanged
    {
        Task AppearingAsync();
        Task DisappearingAsync();
    }
}
