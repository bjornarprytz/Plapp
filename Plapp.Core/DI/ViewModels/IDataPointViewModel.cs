using System;

namespace Plapp.Core
{
    public interface IDataPointViewModel : IViewModel
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        long Value { get; set; }
    }
}
