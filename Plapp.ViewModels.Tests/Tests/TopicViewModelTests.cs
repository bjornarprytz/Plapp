using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
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
    public class TopicViewModelTests : IOViewModelTests<TopicViewModel>
    {
        private Mock<ICamera> cameraMock;
        private Mock<INavigator> navigatorMock;
        private Mock<ITagService> tagServiceMock;
        private Mock<IDataSeriesService> dataSeriesServiceMock;
        private Mock<ITopicService> topicServiceMock;
        private Mock<IPrompter> prompterMock;
        private Mock<IViewModelFactory> vmFactoryMock;
        private Mock<ILogger> loggerMock;

        protected override void FreezeFixtures()
        {
            base.FreezeFixtures();
            
            cameraMock = _fixture.Freeze<Mock<ICamera>>();
            navigatorMock = _fixture.Freeze<Mock<INavigator>>();
            tagServiceMock = _fixture.Freeze<Mock<ITagService>>();
            vmFactoryMock = _fixture.Freeze<Mock<IViewModelFactory>>();
            topicServiceMock = _fixture.Freeze<Mock<ITopicService>>();
            dataSeriesServiceMock = _fixture.Freeze<Mock<IDataSeriesService>>();
            prompterMock = _fixture.Freeze<Mock<IPrompter>>();
            loggerMock = _fixture.Freeze<Mock<ILogger>>();
        }

        [TestMethod]
        public void OnUserInteractionStopped_SaveTopicIsCalled()
        {
            VM.OnUserInteractionStopped();

            topicServiceMock.Verify(s => s.SaveAsync(It.IsAny<Topic>(), default), Times.Once);
        }

        [TestMethod]
        public async Task SaveDataCommand_IdIsZero_UpdatesItsOwnId()
        {
            const int NEW_ID = 1;

            topicServiceMock.Setup(s => s.SaveAsync(It.IsAny<Topic>(), default))
                .Returns(Task.FromResult(new Topic { Id = NEW_ID }));

            await VM.SaveDataCommand.ExecuteAsync();

            VM.Id.Should().Be(NEW_ID);
        }
    }
}
