using System.Windows.Input;

namespace Plapp.Core
{
    public interface IRootViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsSavingData { get; }
        bool IsShowing { get; }

        ICommand LoadDataCommand { get; }
        ICommand SaveDataCommand { get; }

        void OnShow();
        void OnHide();
        void OnUserInteractionStopped();
    }
}
