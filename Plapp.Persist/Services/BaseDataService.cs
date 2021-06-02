using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public abstract class BaseDataService<T> : IDataService<T>
        where T : DomainObject
    {
        protected readonly IDbContextFactory<PlappDbContext> _contextFactory;

        protected BaseDataService(IDbContextFactory<PlappDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            var existing = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (existing != default)
            {
                context.Set<T>().Remove(existing);

                await context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public virtual async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            var existing = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken);

            if (existing != default)
            {
                context.Set<T>().Remove(existing);

                await context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public virtual async Task<IEnumerable<T>> FetchAllAsync(CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T> FetchAsync(int id, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task SaveAllAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().UpdateRange(entities);

            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<T> SaveAsync(T entity, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            var result = context.Set<T>().Update(entity);

            await context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }
    }
}
