﻿using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class TagViewModel : BaseViewModel, ITagViewModel
    {
        public TagViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }

        internal void Hydrate(Tag tagData)
        {
            Id = tagData.Id;
            Key = tagData.Key;
            Unit = tagData.Unit;
            Color = tagData.Color;
            DataType = tagData.DataType;
            Icon = tagData.Icon;
        }
    }
}
