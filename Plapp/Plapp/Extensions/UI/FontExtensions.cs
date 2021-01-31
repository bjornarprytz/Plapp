
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Plapp
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
