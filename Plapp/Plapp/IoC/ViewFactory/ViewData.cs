using System;
using Xamarin.Forms;

namespace Plapp
{
    public class ViewData
    {
        public Type ViewType { get; set; }
        public Type ViewModelType { get; set; }
        public bool IsDetail { get; set; }
        public Func<Page> Creator { get; set; }
    }
}
