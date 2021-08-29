using Plapp.Core;

namespace Plapp.ViewModels
{
    public class TagViewModel : BaseViewModel, ITagViewModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public string IconUri { get; set; }
        public string Color { get; set; }
    }
}
