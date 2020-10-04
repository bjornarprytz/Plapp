﻿
using Microsoft.EntityFrameworkCore;
using Plapp.Core;

namespace Plapp.Relational
{
    public class PlappDbContext : DbContext
    {
        public PlappDbContext(DbContextOptions<PlappDbContext> options) : base(options) { }

        public DbSet<DataPoint> DataPoints { get; private set; }
        public DbSet<DataSeries> DataSeries { get; private set; }
        public DbSet<Note> Notes { get; private set; }
        public DbSet<Tag> Tags { get; private set; }
        public DbSet<Topic> Topics { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataPoint>().HasKey(d => d.Id);
            modelBuilder.Entity<DataPoint>().Property(d => d.DataSeriesId).IsRequired();

            modelBuilder.Entity<DataSeries>().HasKey(d => d.Id);
            modelBuilder.Entity<DataSeries>().Property(d => d.TopicId).IsRequired();
            modelBuilder.Entity<DataSeries>().Property(d => d.Tag).IsRequired();

            modelBuilder.Entity<Note>().HasKey(d => d.Id);
            modelBuilder.Entity<Note>().Property(d => d.TopicId).IsRequired();

            modelBuilder.Entity<Tag>().HasKey(d => d.Id);
            modelBuilder.Entity<Tag>().Property(d => d.DataType).IsRequired();
            modelBuilder.Entity<Tag>().Property(d => d.Unit).IsRequired();

            modelBuilder.Entity<Topic>().HasKey(d => d.Id);
        }
    }
}
