namespace Plapp
{
    public class TagViewModel : ITagViewModel
    {
        public int Id { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }
    }
}
