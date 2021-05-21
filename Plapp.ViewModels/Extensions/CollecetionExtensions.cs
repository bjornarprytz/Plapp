using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plapp.ViewModels
{
    public static class CollecetionExtensions
    {
        public static void Update<TSrc, TDst> (
            this ICollection<TDst> collection, 
            IEnumerable<TSrc> domainObjects,
            IMapper mapper, 
            Func<TDst> create,
            Func<TSrc, TDst, bool> compareSrcDst)
            where TDst : class
        {
            var toAdd = new List<TDst>();
            var toRemove = new List<TDst>(collection);

            foreach (var _do in domainObjects)
            {
                var existing = collection.FirstOrDefault(o => compareSrcDst(_do, o));

                if (existing == default)
                {
                    existing = create();
                    toAdd.Add(existing);
                }
                else
                {
                    toRemove.Remove(existing);
                }

                mapper.Map(_do, existing); // Hydrate
            }

            collection.AddRange(toAdd);
            collection.RemoveRange(toRemove);

        }

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

        public static C RemoveRange<C, T>(this C collection, IEnumerable<T> items)
            where C : ICollection<T>
        {
            if (items == null)
            {
                return collection;
            }

            foreach (var item in items)
            {
                collection.Remove(item);
            }

            return collection;
        }
    }
}
