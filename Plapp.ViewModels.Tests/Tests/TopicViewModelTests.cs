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
        private Mock<ViewModelFactory<IDataSeriesViewModel>> dataSeriesFactoryMock;
        private Mock<ViewModelFactory<ITagViewModel>> tagFactoryMock;
        private Mock<ILogger> loggerMock;

        protected override TopicViewModel SetUpVM()
        {
            cameraMock = new Mock<ICamera>();
            navigatorMock = new Mock<INavigator>();
            tagServiceMock = new Mock<ITagService>();
            dataSeriesFactoryMock = new Mock<ViewModelFactory<IDataSeriesViewModel>>();
            topicServiceMock = new Mock<ITopicService>();
            dataSeriesServiceMock = new Mock<IDataSeriesService>();
            prompterMock = new Mock<IPrompter>();
            tagFactoryMock = new Mock<ViewModelFactory<ITagViewModel>>();
            loggerMock = new Mock<ILogger>();

            return new TopicViewModel(
                cameraMock.Object,
                navigatorMock.Object,
                tagServiceMock.Object,
                dataSeriesServiceMock.Object,
                topicServiceMock.Object,
                prompterMock.Object,
                dataSeriesFactoryMock.Object,
                loggerMock.Object,
                mapper
                );
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
