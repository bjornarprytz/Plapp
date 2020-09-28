﻿namespace Plapp
{
    public class TagViewModel : BaseViewModel, ITagViewModel
    {
        public string Id { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }
    }
}
