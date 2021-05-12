using Plapp.Core;

namespace Plapp.ViewModels
{
    public class TagViewModel : BaseViewModel, ITagViewModel, IHydrate<Tag>
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }

        public void Hydrate(Tag domainObject)
        {
            Id = domainObject.Id;
            Key = domainObject.Key;
            Unit = domainObject.Unit;
            Color = domainObject.Color;
            DataType = domainObject.DataType;
            Icon = domainObject.Icon;
        }
    }
}
