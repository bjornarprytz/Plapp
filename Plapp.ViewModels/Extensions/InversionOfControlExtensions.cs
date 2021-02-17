using System;

namespace Plapp.ViewModels
{
    public static class InversionOfControlExtensions
    {
        public static T Get<T>(this IServiceProvider serviceProvider) => (T)serviceProvider.GetService(typeof(T));
    }
}
