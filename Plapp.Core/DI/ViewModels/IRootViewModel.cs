namespace Plapp.Core
{
    public interface IRootViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsSavingData { get; }
        bool IsShowing { get; }

        void OnShow();
        void OnHide();
        void OnUserInteractionStopped();
    }
}
