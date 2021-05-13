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

            Assert.IsTrue(items.Count() == 2);
        }

        [TestMethod]
        public async Task FetchAsync_EntityExists_ReturnsIt()
        {
            var item1 = CreateStub(id: 1);
            var item2 = CreateStub(id: 2);

            SeedDbWith(item1, item2);

            var item = await dataService.FetchAsync(1);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == 1);
        }

        [TestMethod]
        public async Task FetchAsync_EntityDoesNotExist_ReturnsNull()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            var item = await dataService.FetchAsync(69);

            Assert.IsNull(item);
        }

        [TestMethod]
        public async Task DeleteAsync_ById_EntityExists_DeletesIt()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            await dataService.DeleteAsync(1);

            using var context = new PlappDbContext(options);

            Assert.IsFalse(context.Set<T>().Any(item => item.Id == 1));
        }

        [TestMethod]
        public async Task DeleteAsync_ByEntity_EntityExists_DeletesIt()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            await dataService.DeleteAsync(item1);

            using var context = new PlappDbContext(options);

            Assert.IsFalse(context.Set<T>().Any(item => item.Id == 1));
        }

        [TestMethod]
        public async Task DeleteAsync_ById_EntityDoesNotExist_Noop()
        {
            await dataService.DeleteAsync(1);

            using var context = new PlappDbContext(options);

            Assert.IsFalse(context.Set<T>().Any());
        }

        [TestMethod]
        public async Task DeleteAsync_ByEntity_EntityDoesNotExist_Noop()
        {
            var item1 = CreateStub(id: 1);

            await dataService.DeleteAsync(item1);

            using var context = new PlappDbContext(options);

            Assert.IsFalse(context.Set<T>().Any());
        }

        [TestMethod]
        public async Task SaveAsync_EntityExists_UpdatesIt()
        {
            var item1 = CreateStub(id: 1);

            SeedDbWith(item1);

            var mutatedItem = AlteredCopy(item1);

            await dataService.SaveAsync(mutatedItem);

            using var context = new PlappDbContext(options);

            var result = context.Set<T>().FirstOrDefault(item => item.Id == item1.Id);

            Assert.IsNotNull(result);
            result.Should().BeEquivalentTo(mutatedItem);
        }

        [TestMethod]
        public async Task SaveAsync_IdIsZero_AddsIt()
        {
            var item1 = CreateStub(id: 0);

            await dataService.SaveAsync(item1);

            using var context = new PlappDbContext(options);

            var result = context.Set<T>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsFalse(result.Any(item => item.Id == 0));
        }

        [TestMethod]
        public async Task SaveAllAsync_IdsAreZero_AddsThem()
        {
            var item1 = CreateStub(id: 0);
            var item2 = CreateStub(id: 0);

            await dataService.SaveAllAsync(new List<T>() { item1, item2 });

            using var context = new PlappDbContext(options);

            var result = context.Set<T>();

            Assert.IsTrue(result.Count() == 2);
            Assert.IsFalse(result.Any(item => item.Id == 0));
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

            Assert.IsTrue(context.Set<T>().Count() == 2);

            Assert.IsFalse(context.Set<T>().Any(item => item.Id == 0));

            var result1 = context.Set<T>().First(item => item.Id == item1.Id);

            result1.Should().BeEquivalentTo(mutatedItem);
            
            var result2 = context.Set<T>().First(item => item.Id == item2.Id);

            result2.Should().BeEquivalentTo(item2);
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

            context.SaveChanges();
        }

        private void DeleteDatabase()
        {
            using var context = new PlappDbContext(options);

            context.Database.EnsureDeleted();
        }
    }
}
