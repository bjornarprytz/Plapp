﻿using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;
using System.Threading.Tasks;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public class ApplicationViewModelTests : BaseViewModelTests<ApplicationViewModel>
    {
        private Mock<INavigator> navigatorMock;
        private Mock<ViewModelFactory<ITopicViewModel>> topicFactoryMock;
        private Mock<ITopicService> topicServiceMock;


        protected override void FreezeFixtures()
        {
            base.FreezeFixtures();

            navigatorMock = _fixture.Freeze<Mock<INavigator>>();
            topicFactoryMock = _fixture.Freeze<Mock<ViewModelFactory<ITopicViewModel>>>();
            topicServiceMock = _fixture.Freeze<Mock<ITopicService>>();
        }

        [TestMethod]
        public async Task AddTopicCommand_FactoryCalledOnce()
        {
            var t = _fixture.Freeze<Mock<ViewModelFactory<ITopicViewModel>>>();

            await VM.AddTopicCommand.ExecuteAsync();

            t.Verify(f => f(), Times.Once);
        }

        [TestMethod]
        public async Task AddTopicCommand_TopicAddedToCollection()
        {
            var topicMock = _fixture.Freeze<Mock<ITopicViewModel>>();
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            var topicCount = VM.Topics.Count;

            await VM.AddTopicCommand.ExecuteAsync();

            VM.Topics.Should().HaveCount(topicCount + 1);
            VM.Topics.Should().Contain(topicMock.Object);
        }

        [TestMethod]
        public async Task AddTopicCommand_TopicSavedToService()
        {
            await VM.AddTopicCommand.ExecuteAsync();

            topicServiceMock.Verify(s => s.SaveAsync(It.IsAny<Topic>(), default));
        }

        [TestMethod]
        public async Task AddTopicCommand_NavigatesToTopicOnce()
        {
            var topicMock = _fixture.Freeze <Mock<ITopicViewModel>>();
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            await VM.AddTopicCommand.ExecuteAsync();

            navigatorMock.Verify(n => n.GoToAsync(It.Is<ITopicViewModel>(t => t == topicMock.Object)), Times.Once);
        }

        [TestMethod]
        public async Task DeleteTopicCommand_TopicRemovedFromCollection()
        {
            var topicMock = _fixture.Freeze<Mock<ITopicViewModel>>();
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            await VM.AddTopicCommand.ExecuteAsync();

            await VM.DeleteTopicCommand.ExecuteAsync(topicMock.Object);

            VM.Topics.Should().BeEmpty();
        }

        [TestMethod]
        public async Task DeleteTopicCommand_TopicDeletedFromService()
        {
            var topicMock = _fixture.Freeze<Mock<ITopicViewModel>>();

            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            await VM.AddTopicCommand.ExecuteAsync();

            await VM.DeleteTopicCommand.ExecuteAsync(topicMock.Object);

            topicServiceMock.Verify(s => s.DeleteAsync(It.IsAny<Topic>(), default));
        }
    }
}
