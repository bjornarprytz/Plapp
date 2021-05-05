using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;
using System;

namespace Plapp.Persist.Tests
{
    [TestClass]
    public class TopicServiceTests : BaseDataServiceTests<TopicService, Topic>
    {
        protected override Topic AlteredCopy(Topic stub)
        {
            var topic = new Topic
            {
                Id = stub.Id,
                Title = "Title",
                Description = "Description",
                ImageUri = "image.jpg",
            };

            return topic;
        }

        protected override Topic CreateStub(int id = 0)
        {
            var topic = new Topic 
            { 
                Id = id
            };

            return topic;
        }

        protected override TopicService CreateTestableService(IServiceProvider sp)
        {
            return new TopicService(sp);
        }
    }
}
