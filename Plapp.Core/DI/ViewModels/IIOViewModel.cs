using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface IIOViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsSavingData { get; }

        IAsyncCommand LoadDataCommand { get; }
        IAsyncCommand SaveDataCommand { get; }
    }
}
