using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;
using System;
using System.Threading.Tasks;

namespace Plapp.Persist.Tests
{
    [TestClass]
    public class TagServiceTests : BaseDataServiceTests<TagService, Tag>
    {
        [TestMethod]
        public async Task FetchAsync_ByKey_TagExists_ReturnsIt()
        {
            var item1 = new Tag { Id = 1, Key = "dirt" };
            var item2 = new Tag { Id = 2, Key = "light" };

            SeedDbWith(item1, item2);

            var item = await dataService.FetchAsync("dirt");

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == 1 && item.Key == "dirt");
        }

        [TestMethod]
        public async Task FetchAsync_ByKey_TagDoesNotExist_ReturnsNull()
        {
            var item1 = new Tag { Id = 2, Key = "light" };

            SeedDbWith(item1);

            var item = await dataService.FetchAsync("dirt");

            Assert.IsNull(item);
        }


        protected override Tag AlteredCopy(Tag stub)
        {
            return new Tag 
            {
                Id = stub.Id,
                Key = "water",
                Color = "asd",
                DataType = DataType.Decimal,
                Icon = Icon.Diamond,
                Unit = "ml",
            };
        }

        protected override Tag CreateStub(int id = 0)
        {
            var tag = new Tag { Id = id };
            
            return tag;
        }

        protected override TagService CreateTestableService(IServiceProvider sp)
        {
            return new TagService(sp);
        }
    }
}
