using System.Collections.Generic;

namespace Plapp.ViewModels
{
    public static class CollecetionExtensions
    {
        public static C AddRange<C, T>(this C collection, IEnumerable<T> items)
            where C : ICollection<T>
        { 
            if (items == null)
            {
                return collection;
            }

            foreach( var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
