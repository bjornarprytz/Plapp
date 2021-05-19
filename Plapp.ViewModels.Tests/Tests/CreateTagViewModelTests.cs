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

        protected override CreateTagViewModel SetUpVM()
        {
            base.SetUpVM();

            tagServiceMock = new Mock<ITagService>();

            return new CreateTagViewModel(
                factoryMock.Object,
                prompterMock.Object,
                tagServiceMock.Object,
                mapper
                );
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
            factoryMock.Setup(f => f()).Returns(() => new Mock<ITagViewModel>().Object);

            var tag1 = new Tag { Id = 1 };
            var tag2 = new Tag { Id = 2 };

            var tags = new List<Tag> { tag1, tag2 };

            tagServiceMock.Setup(s => s.FetchAllAsync(default)).Returns(Task.FromResult(tags.AsEnumerable()));

            await VM.LoadDataCommand.ExecuteAsync();

            VM.AvailableTags.Should().Contain(tag => tag.Id == 1);
            VM.AvailableTags.Should().Contain(tag => tag.Id == 2);
        }

        // TODO: Test OnShow(), that LoadAvailableTags is called
    }
}
