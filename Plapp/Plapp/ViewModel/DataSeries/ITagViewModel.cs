namespace Plapp
{
    public interface ITagViewModel : IViewModel, IData
    {
        string Unit { get; set; }
        string Name { get; set; }
        DataType DataType { get; set; }
        Icon Icon { get; set; }
        string Color { get; set; }
    }
}
