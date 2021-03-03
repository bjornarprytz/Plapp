namespace Plapp.Core
{
    public interface IRootViewModel : IViewModel
    {
        bool IsShowing { get; }

        void OnShow();
        void OnHide();
        void OnUserInteractionStopped();
    }
}
