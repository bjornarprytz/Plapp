using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plapp.ViewModels.Tests
{
    public static class Extensions
    {
        public static void ListenToPropertyChanged<T>(this T thing, string propertyName, Action result)
            where T : INotifyPropertyChanged
        {
            thing.PropertyChanged += Delegate;

            void Delegate(object s, PropertyChangedEventArgs args)
            {
                if (args.PropertyName == propertyName)
                {
                    result();
                    thing.PropertyChanged -= Delegate;
                }
            }
        }
    }
}
