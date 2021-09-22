using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Plapp.UI.Controls
{
    public class EnumPicker<T> : Picker
        where T : Enum
    {
        public EnumPicker()
        {
            ItemsSource = Enum.GetNames(typeof(T)).ToList();
        }
        
        
    }
}