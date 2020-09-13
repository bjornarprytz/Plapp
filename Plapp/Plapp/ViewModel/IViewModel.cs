using System;
using System.ComponentModel;

namespace Plapp
{
    public interface IViewModel : INotifyPropertyChanged
    {
        void SetState<T>(Action<T> action) where T : class, IViewModel;
    }
}
