using Microsoft.EntityFrameworkCore;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.Persist
{
    public class DataSeriesService : BaseDataService<DataSeries>, IDataSeriesService
    {
        public DataSeriesService(IDbContextFactory<PlappDbContext> contextFactory) : base(contextFactory) { }

        public async Task<IEnumerable<DataSeries>> FetchAllAsync(int? topicId = null, int? tagId = null, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<DataSeries>().Where(
                d => (tagId == null || d.TagId == tagId)
                  && (topicId == null || d.TopicId == topicId))
                .Include(d => d.DataPoints)
                .Include(d => d.Tag)
                .ToListAsync(cancellationToken);
        }

        public async override Task<IEnumerable<DataSeries>> FetchAllAsync(CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<DataSeries>()
                .Include(d => d.DataPoints)
                .Include(d => d.Tag)
                .ToListAsync(cancellationToken);
        }

        public async override Task<DataSeries> FetchAsync(int id, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<DataSeries>()
                .Include(d => d.DataPoints)
                .Include(d => d.Tag)
                .FirstOrDefaultAsync(ds => ds.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<DataPoint>> FetchDataPointsAsync(int dataSeriesId, CancellationToken cancellationToken = default)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<DataPoint>()
                .Where(dp => dp.DataSeriesId == dataSeriesId)
                .ToListAsync(cancellationToken);
        }
    }
}
