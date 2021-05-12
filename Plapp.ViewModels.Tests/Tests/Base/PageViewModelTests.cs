using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class PageViewModelTests<TViewModel> : BaseViewModelTest<TViewModel>
        where TViewModel : BaseViewModel, IRootViewModel
    {

        [TestMethod]
        public void OnShow_IsShowingIsTrue()
        {
            VM.OnShow();

            VM.IsShowing.Should().BeTrue();
        }

        [TestMethod]
        public void OnHide_IsShowingIsFalse()
        {
            VM.OnHide();

            VM.IsShowing.Should().BeFalse();
        }

        // TODO: Write tests for save / load data OnHide, OnShow and OnUserInteractionStopped
    }
}
