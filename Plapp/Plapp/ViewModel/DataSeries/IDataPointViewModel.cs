using System;
using System.ComponentModel;

namespace Plapp
{
    public interface IDataPointViewModel : INotifyPropertyChanged
    {
        bool HasData { get; }
        DateTime Date { get; }
        long Data { get; }
    }
}
