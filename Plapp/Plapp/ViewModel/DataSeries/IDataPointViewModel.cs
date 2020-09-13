using System;
using System.ComponentModel;

namespace Plapp
{
    public interface IDataPointViewModel : IViewModel
    {
        bool HasData { get; }
        DateTime Date { get; }
        long Data { get; }
    }
}
