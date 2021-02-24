using System;

namespace Plapp
{
    public static class InversionOfControlExtensions
    {
        public static T Get<T>(this IServiceProvider serviceProvider) => (T)serviceProvider.GetService(typeof(T));
    }
}
