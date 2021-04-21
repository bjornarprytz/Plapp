namespace Plapp.Core
{
    public record Tag
    {
        public int Id { get; init; }
        public string Key { get; init; }
        public string Unit { get; init; }
        public DataType DataType { get; init; }
        public Icon Icon { get; init; }
        public string Color { get; init; }
    }
}
