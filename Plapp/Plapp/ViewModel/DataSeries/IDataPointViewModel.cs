using System;

namespace Plapp
{
    public interface IDataPointViewModel : IViewModel
    {
        DateTime Date { get; }
        long Data { get; }
    }
}
