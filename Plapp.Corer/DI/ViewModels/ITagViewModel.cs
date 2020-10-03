namespace Plapp.Core
{
    public interface ITagViewModel : IViewModel
    {
        string Id { get; set; }
        string Unit { get; set; }
        DataType DataType { get; set; }
        Icon Icon { get; set; }
        string Color { get; set; }
    }
}
