using System;

namespace Plapp.Core
{
    public interface IDataPointViewModel : IViewModel
    {
        int Id { get; }
        DateTime Date { get; }
        object Data { get; }
    }
}
