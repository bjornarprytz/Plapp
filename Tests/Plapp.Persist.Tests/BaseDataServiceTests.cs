using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plapp.Persist.Tests
{
    [TestClass]
    public abstract class BaseDataServiceTests<TService, T> 
        where TService : BaseDataService<T> 
        where T : DomainObject
    {
        protected TService dataService;
        protected readonly DbContextOptions<PlappDbContext> options = new DbContextOptionsBuilder<PlappDbContext>()
                                                                .UseInMemoryDatabase(databaseName: "PlappDb")
                                                                .Options;

        protected Mock<IDbContextFactory<PlappDbContext>> contextFactoryMock;

        [TestInitialize]
        public virtual void Initialize()
        {
            contextFactoryMock = new Mock<IDbContextFactory<PlappDbContext>>();

            contextFactoryMock.Setup(f => f.CreateDbContext()).Returns(() => new PlappDbContext(options));

            dataService = CreateTestableService(contextFactoryMock.Object);

            DeleteDatabase();
        }

        [TestMethod]
        public async Task FetchAllAsync_ReturnsAllEntities()
        {
            SeedDb(2);

            var items = await dataService.FetchAllAsync();

            items.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task FetchAsync_EntityExists_ReturnsIt()
        {
            var item1 = CreateStub(id: 1);
            var item2 = CreateStub(id: 2);

            SeedDbWith(item1, item2);

            var item = await dataService.FetchAsync(1);

            item.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task FetchAsync_EntityDoesNotExist_ReturnsNull()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            var item = await dataService.FetchAsync(69);

            item.Should().BeNull();
        }

        [TestMethod]
        public async Task DeleteAsync_ById_EntityExists_DeletesIt()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            await dataService.DeleteAsync(1);

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().NotContain(item => item.Id == 1);
        }

        [TestMethod]
        public async Task DeleteAsync_ByEntity_EntityExists_DeletesIt()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            await dataService.DeleteAsync(item1);

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().NotContain(item => item.Id == 1);
        }

        [TestMethod]
        public async Task DeleteAsync_ById_EntityDoesNotExist_Noop()
        {
            await dataService.DeleteAsync(1);

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().BeEmpty();
        }

        [TestMethod]
        public async Task DeleteAsync_ByEntity_EntityDoesNotExist_Noop()
        {
            var item1 = CreateStub(id: 1);

            await dataService.DeleteAsync(item1);

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().BeEmpty();
        }

        [TestMethod]
        public async Task SaveAsync_EntityExists_UpdatesIt()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            var mutatedItem = AlteredCopy(item1);

            await dataService.SaveAsync(mutatedItem);

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().HaveCount(1);
            context.Set<T>().Should().ContainEquivalentOf(mutatedItem);
        }

        [TestMethod]
        public async Task SaveAsync_IdIsZero_AddsItWithNewId()
        {
            var item1 = CreateStub(id: 0);

            await dataService.SaveAsync(item1);

            using var context = new PlappDbContext(options);

            var result = context.Set<T>();

            result.Should().HaveCount(1);
            result.Should().NotContain(item => item.Id == 0);
        }

        [TestMethod]
        public async Task SaveAllAsync_IdsAreZero_AddsThemWithNewIds()
        {
            var item1 = CreateStub(id: 0);
            var item2 = CreateStub(id: 0);

            await dataService.SaveAllAsync(new List<T>() { item1, item2 });

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().HaveCount(2);
            context.Set<T>().Should().NotContain(item => item.Id == 0);
        }

        [TestMethod]
        public async Task SaveAllAsync_OneNew_OneExisting_TwoItemsInDb()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            var mutatedItem = AlteredCopy(item1);

            var item2 = AlteredCopy(CreateStub(id: 0));

            await dataService.SaveAllAsync(new List<T>() { mutatedItem, item2 });

            using var context = new PlappDbContext(options);

            context.Set<T>().Should().HaveCount(2);

            context.Set<T>().Should().ContainEquivalentOf(mutatedItem);
            context.Set<T>().Should().ContainEquivalentOf(item2);
        }

        protected abstract TService CreateTestableService(IDbContextFactory<PlappDbContext> contextFactory);
        protected abstract T CreateStub(int id=0);
        protected abstract T AlteredCopy(T stub);
        protected void SeedDbWith(params T[] stubs)
        {
            SeedDb(stubs);
        }

        protected void SeedDbWith<TOther>(params TOther[] stubs) where TOther : DomainObject
        {
            using var context = new PlappDbContext(options);

            context.Set<TOther>().AddRange(stubs);

            context.SaveChanges();
        }

        protected void SeedDb(int numStubs)
        {
            SeedDb(Enumerable.Range(1, numStubs)
                    .Select(i => CreateStub(i)));
        }

        protected void SeedDb(IEnumerable<T> stubs)
        {
            using var context = new PlappDbContext(options);

            context.Set<T>().AddRange(stubs);

            var c = context.SaveChanges();
        }

        private void DeleteDatabase()
        {
            using var context = new PlappDbContext(options);

            context.Database.EnsureDeleted();
        }
    }
}
