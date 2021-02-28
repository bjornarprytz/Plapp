﻿using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TagBadge : BaseContentView<ITagViewModel>
    {
        public TagBadge()
        {
            Content = new Label()
                        .Bind(nameof(VM.Id))
                        .Bind(BackgroundColorProperty, nameof(VM.Color), convert: (string s) => s == null ? Palette.Information : Color.FromHex(s));
        }
    }
}
