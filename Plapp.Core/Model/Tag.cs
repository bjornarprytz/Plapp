﻿namespace Plapp.Core
{
    public class Tag : DomainObject
    {
        public string Key { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }
    }
}
