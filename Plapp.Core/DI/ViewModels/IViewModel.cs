using System;
using System.ComponentModel;

namespace Plapp.Core
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsShowing { get; }

        void OnShow();
        void OnHide();
        void OnUserInteractionStopped();
    }
}
