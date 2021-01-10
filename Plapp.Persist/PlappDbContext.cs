
using Microsoft.EntityFrameworkCore;
using Plapp.Core;

namespace Plapp.Persist
{
    public class PlappDbContext : DbContext
    {
        public PlappDbContext(DbContextOptions<PlappDbContext> options) : base(options) { }

        public DbSet<DataPoint> DataPoints { get; private set; }
        public DbSet<DataSeries> DataSeries { get; private set; }
        public DbSet<Tag> Tags { get; private set; }
        public DbSet<Topic> Topics { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>().HasKey(d => d.Id);
            modelBuilder.Entity<Tag>().Property(d => d.DataType).IsRequired();
            modelBuilder.Entity<Tag>().Property(d => d.Unit).IsRequired();


            modelBuilder.Entity<DataPoint>().HasKey(d => d.Id);
            modelBuilder.Entity<DataPoint>().Property(d => d.Date).IsRequired();
            modelBuilder.Entity<DataPoint>().Property(d => d.Value).IsRequired();
            modelBuilder.Entity<DataPoint>().Property(d => d.DataSeriesId).IsRequired();

            modelBuilder.Entity<DataSeries>().HasKey(d => d.Id);
            modelBuilder.Entity<DataSeries>().Property(d => d.TopicId).IsRequired();
            modelBuilder.Entity<DataSeries>().Property(d => d.TagId).IsRequired();


            modelBuilder.Entity<Topic>().HasKey(d => d.Id);
        }
    }
}
