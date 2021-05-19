using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class BaseCreateMultipleViewModelsTests<TViewModel, TTemplate> : BaseTaskViewModelTests<TViewModel>
        where TViewModel : BaseCreateMultipleViewModel<TTemplate>
        where TTemplate : class, IViewModel
    {
        [TestMethod]
        public virtual void ConfirmCurrentCommand_CurrentIsAddedToPartials()
        {
            var vmMock = new Mock<TTemplate>();
            VM.Current = vmMock.Object;

            var count = VM.Partials.Count;

            VM.ConfirmCurrentCommand.Execute(null);

            VM.Partials.Should().HaveCount(count + 1);
        }

        [TestMethod]
        public virtual void ConfirmCurrentCommand_NewCurrentIsFromTemplate()
        {
            var vmMock = new Mock<TTemplate>();

            VM.TemplateFactory = () => vmMock.Object;

            VM.ConfirmCurrentCommand.Execute(null);

            VM.Current.Should().Be(vmMock.Object);
        }

        [TestMethod]
        public virtual void ConfirmCurrentCommand_BackToPreviousCommand_CanExecute_IsChanged()
        {
            bool canExecuteChanged = false;

            VM.BackToPreviousCommand.CanExecuteChanged += (s, e) => canExecuteChanged = true;

            VM.ConfirmCurrentCommand.Execute(null);

            canExecuteChanged.Should().BeTrue();
        }

        [TestMethod]
        public virtual void PartialsIsEmpty_BackToPreviousCommand_CanExecute_IsFalse()
        {
            VM.Partials.Clear();

            VM.BackToPreviousCommand.CanExecute(null).Should().BeFalse();
        }
        
        [TestMethod]
        public virtual void PartialsIsNotEmpty_BackToPreviousCommand_CanExecute_IsTrue()
        {
            var vmMock = new Mock<TTemplate>();
            VM.Partials.Add(vmMock.Object);

            VM.BackToPreviousCommand.CanExecute(null).Should().BeTrue();
        }

        [TestMethod]
        public virtual void BackToPreviousCommand_LastPartialIsCurrent()
        {
            var vmMock = new Mock<TTemplate>();
            VM.Partials.Add(vmMock.Object);

            VM.BackToPreviousCommand.Execute(null);

            VM.Current.Should().Be(vmMock.Object);
        }

        [TestMethod]
        public virtual void BackToPreviousCommand_LastPartialRemovedFromPartials()
        {
            var vmMock = new Mock<TTemplate>();
            VM.Partials.Add(vmMock.Object);

            VM.BackToPreviousCommand.Execute(null);

            VM.Partials.Should().NotContain(vmMock.Object);
        }

        [TestMethod]
        public virtual void ConfirmCommand_GetResultShouldContainAllPartialsInOrder()
        {
            var vmMock1 = new Mock<TTemplate>();
            var vmMock2 = new Mock<TTemplate>();
            
            VM.Partials.Add(vmMock1.Object);
            VM.Partials.Add(vmMock2.Object);

            VM.ConfirmCommand.Execute(null);

            VM.GetResult().Should().ContainInOrder(vmMock1.Object, vmMock2.Object);
        }

        [TestMethod]
        public virtual void ConfirmCommand_GetResultShouldContainCurrent()
        {
            var vmMock = new Mock<TTemplate>();

            VM.Current = vmMock.Object;

            VM.ConfirmCommand.Execute(null);

            VM.GetResult().Should().Contain(vmMock.Object);
        }

        [TestMethod]
        public virtual void ConfirmCommand_CanConfirmIsFalse_CanExecuteIsFalse()
        {
            VM.Current = null;
            VM.Partials.Clear();

            VM.ConfirmCommand.CanExecute(null).Should().BeFalse();
        }

        [TestMethod]
        public virtual void ConfirmCommand_CurrentIsNotNull_CanExecuteIsTrue()
        {
            var vmMock = new Mock<TTemplate>();

            VM.Current = vmMock.Object;

            VM.ConfirmCommand.CanExecute(null).Should().BeTrue();
        }

        [TestMethod]
        public virtual void ConfirmCommand_PartialsIsNotEmpty_CanExecuteIsTrue()
        {
            var vmMock = new Mock<TTemplate>();

            VM.Partials.Add(vmMock.Object);

            VM.ConfirmCommand.CanExecute(null).Should().BeTrue();
        }
    }
}
