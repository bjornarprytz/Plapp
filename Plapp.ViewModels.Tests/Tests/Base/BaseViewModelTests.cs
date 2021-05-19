using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;
using System;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class BaseViewModelTests<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected TViewModel VM;

        [TestInitialize]
        public void Initialize()
        {
            VM = SetUpVM();
        }

        protected abstract TViewModel SetUpVM();


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
    }
}
