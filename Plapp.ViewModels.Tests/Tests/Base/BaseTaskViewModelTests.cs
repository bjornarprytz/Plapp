using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class BaseTaskViewModelTests<TViewModel> : PageViewModelTests<TViewModel>
        where TViewModel : BaseTaskViewModel
    {
        protected Mock<IPrompter> _prompterMock;

        protected override TViewModel SetUpVM()
        {
            _prompterMock = new Mock<IPrompter>();

            return null;
        }

        [TestMethod]
        public virtual void ConfirmCommand_IsConfirmed_IsTrue()
        {
            VM.ConfirmCommand.Execute(null);

            VM.IsConfirmed.Should().BeTrue();
        }

        [TestMethod]
        public virtual void ConfirmCommand_Prompt_IsPoppedOnce()
        {
            VM.ConfirmCommand.Execute(null);

            _prompterMock.Verify(p => p.PopAsync(), Times.Once);
        }

        [TestMethod]
        public virtual void CancelCommand_Prompt_IsPoppedOnce()
        {
            VM.CancelCommand.Execute(null);

            _prompterMock.Verify(p => p.PopAsync(), Times.Once);
        }
    }
}
