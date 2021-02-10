using Dna;
using System;

namespace Plapp
{
    public static class IoC
    {
        public static T Get<T>() 
            => Framework.Service<T>();

        public static T Resolve<T>(Type type) where T : class
            => Framework.Provider.GetService(type) as T;

        public static object Resolve(Type type) => Framework.Provider.GetService(type);
    }
}
