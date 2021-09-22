using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITagViewModel : IViewModel
    {
        int Id { get; set; }
        string Key { get; set; }
        string Unit { get; set; }
        DataType DataType { get; set; }
        string IconUri { get; set; }
        string Color { get; set; }
    }
}
