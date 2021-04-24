using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITagViewModel : IViewModel
    {
        int Id { get; }
        string Key { get; set; }
        string Unit { get; set; }
        DataType DataType { get; set; }
        Icon Icon { get; set; }
        string Color { get; set; }

        ICommand SaveCommand {get;}
    }
}
