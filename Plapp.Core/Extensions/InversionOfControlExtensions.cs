using System;

namespace Plapp
{
    public static class InversionOfControlExtensions
    {
        public static T Get<T>(this IServiceProvider serviceProvider) => (T)serviceProvider.GetService(typeof(T));
        public static T Get<T>(this IServiceProvider serviceProvider, Action<T> setStateAction)
        {
            var thing = (T)serviceProvider.GetService(typeof(T));

            setStateAction?.Invoke(thing);

            return thing;
        }
    }
}
