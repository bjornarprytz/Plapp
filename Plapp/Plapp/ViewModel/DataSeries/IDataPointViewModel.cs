using System;

namespace Plapp
{
    public interface IDataPointViewModel : IViewModel, IData
    {
        DateTime Date { get; }
        long Data { get; }
    }
}
