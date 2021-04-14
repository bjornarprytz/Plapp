using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public static class DbExtensions
    {
        public static async Task AddOrUpdate<T>(this DbSet<T> dbSet, int id, T newEntry)
            where T : class
        {
            var existingEntry = await dbSet.FindAsync(id);

            if (existingEntry != null)
            {
                dbSet.Remove(existingEntry);
            }

            dbSet.Add(newEntry);
        }
    }
}
