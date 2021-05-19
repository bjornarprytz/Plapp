using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public class CreateTagViewModelTests : BaseCreateViewModelTests<CreateTagViewModel, ITagViewModel>
    {
        private Mock<ITagService> tagServiceMock;

        protected override void FreezeFixtures()
        {
            base.FreezeFixtures();

            tagServiceMock = _fixture.Freeze<Mock<ITagService>>();
        }

        [TestMethod]
        public async Task LoadDataCommand_TagsAreLoaded()
        {
            await VM.LoadDataCommand.ExecuteAsync();

            tagServiceMock.Verify(s => s.FetchAllAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task LoadDataCommand_AvailableTagsAreAddedToCollection()
        {
            var tag1 = new Tag { Id = 1 };
            var tag2 = new Tag { Id = 2 };

            var tags = new List<Tag> { tag1, tag2 };

            tagServiceMock.Setup(s => s.FetchAllAsync(default)).Returns(Task.FromResult(tags.AsEnumerable()));

            await VM.LoadDataCommand.ExecuteAsync();

            VM.AvailableTags.Should().HaveCount(2);
        }

        // TODO: Test OnShow(), that LoadAvailableTags is called
    }
}
