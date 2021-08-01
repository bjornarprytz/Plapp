﻿using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Plapp.Views.Extensions.UI
{
    public static class FontExtensions
    {
        public static T NamedFontSize<T>(this T fontElement, NamedSize size)
            where T : Element, IFontElement
        {
            return fontElement.FontSize(Device.GetNamedSize(size, fontElement));
        }

    }
}
