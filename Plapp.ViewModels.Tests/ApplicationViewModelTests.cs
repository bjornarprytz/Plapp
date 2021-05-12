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
    public class ApplicationViewModelTests : BaseViewModelTest<ApplicationViewModel>
    {
        private Mock<INavigator> navigatorMock;
        private Mock<ViewModelFactory<ITopicViewModel>> topicFactoryMock;
        private Mock<ITopicService> topicServiceMock;


        protected override ApplicationViewModel SetUpVM()
        {
            navigatorMock = new Mock<INavigator>();
            topicFactoryMock = new Mock<ViewModelFactory<ITopicViewModel>>();
            topicServiceMock = new Mock<ITopicService>();

            return new ApplicationViewModel(
                navigatorMock.Object,
                topicFactoryMock.Object,
                topicServiceMock.Object
                );
        }

        [TestMethod]
        public void AddTopicCommand_FactoryCalledOnce()
        {
            VM.AddTopicCommand.Execute(null);

            topicFactoryMock.Verify(f => f(), Times.Once);
        }

        [TestMethod]
        public void AddTopicCommand_TopicAddedToCollection()
        {
            var topicMock = new Mock<ITopicViewModel>();
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            var topicCount = VM.Topics.Count;

            VM.AddTopicCommand.Execute(null);

            VM.Topics.Should().HaveCount(topicCount + 1);
            VM.Topics.Should().Contain(topicMock.Object);
        }

        [TestMethod]
        public void AddTopicCommand_TopicSavedToService()
        {
            const int ID = 1;

            var topicMock = new Mock<ITopicViewModel>();
            topicMock.SetupGet(t => t.Id).Returns(ID);
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            VM.AddTopicCommand.Execute(null);

            topicServiceMock.Verify(s => s.SaveAsync(It.Is<Topic>(t => t.Id == ID), default));
        }

        [TestMethod]
        public void AddTopicCommand_NavigatesToTopicOnce()
        {
            var topicMock = new Mock<ITopicViewModel>();
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            VM.AddTopicCommand.Execute(null);

            navigatorMock.Verify(n => n.GoToAsync(It.Is<ITopicViewModel>(t => t == topicMock.Object)), Times.Once);
        }

        [TestMethod]
        public void DeleteTopicCommand_TopicRemovedFromCollection()
        {
            var topicMock = new Mock<ITopicViewModel>();
            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            VM.AddTopicCommand.Execute(null);

            VM.DeleteTopicCommand.Execute(topicMock.Object);

            VM.Topics.Should().BeEmpty();
        }

        [TestMethod]
        public void DeleteTopicCommand_TopicDeletedFromService()
        {
            const int ID = 1;

            var topicMock = new Mock<ITopicViewModel>();
            topicMock.SetupGet(t => t.Id).Returns(ID);

            topicFactoryMock.Setup(f => f()).Returns(() => topicMock.Object);

            VM.AddTopicCommand.Execute(null);

            VM.DeleteTopicCommand.Execute(topicMock.Object);

            topicServiceMock.Verify(s => s.DeleteAsync(It.Is<Topic>(t => t.Id == ID), default));
        }
    }
}
