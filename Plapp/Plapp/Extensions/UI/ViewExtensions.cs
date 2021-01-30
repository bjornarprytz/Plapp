using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plapp
{
    public static class ViewExtensions
    {
        public static T VerticalOptions<T>(this T view, LayoutOptions layout)
            where T : View
        {
            view.VerticalOptions = layout;
            return view;
        }
    }
}
