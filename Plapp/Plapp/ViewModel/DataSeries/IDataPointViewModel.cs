using System;

namespace Plapp
{
    public interface IDataPointViewModel : IViewModel
    {
        int Id { get; }
        DateTime Date { get; }
        object Data { get; }
    }
}
