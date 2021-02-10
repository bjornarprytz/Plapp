using System;
using System.ComponentModel;

namespace Plapp.Core
{
    public interface IViewModel : INotifyPropertyChanged
    {
        void SetState<T>(Action<T> action) where T : class, IViewModel;

        bool IsShowing { get; }

        void OnShow();
        void OnHide();
        void OnUserInteractionStopped();
    }
}
