using System;

namespace Plapp.Core
{
    public interface IDataPointViewModel : IViewModel
    {
        DataType DataType { get; set; }
        int Id { get; set; }
        DateTime Date { get; set; }
        long Value { get; set; }
    }
}
