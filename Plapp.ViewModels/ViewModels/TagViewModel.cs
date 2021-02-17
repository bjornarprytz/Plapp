using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class TagViewModel : BaseViewModel, ITagViewModel
    {
        public TagViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        public string Id { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }
    }
}
