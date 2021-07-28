using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plapp.ViewModels
{
    public static class CollectionExtensions
    {
        public static void Update<T> (
            this ICollection<T> stale, 
            IEnumerable<T> fresh,
            Func<T, T, bool> compareItems)
            where T : class
        {
            var toAdd = new List<T>();
            var toRemove = new List<T>(stale);

            foreach (var freshItem in fresh)
            {
                var existing = stale.FirstOrDefault(staleItem => compareItems(freshItem, staleItem));

                if (existing == default)
                {
                    toAdd.Add(freshItem);
                }
                else
                {
                    // TODO: Update existing with freshItem? New data will be "ignored" here currently

                    toRemove.Remove(existing);
                }
            }

            stale.AddRange(toAdd);
            stale.RemoveRange(toRemove);

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
