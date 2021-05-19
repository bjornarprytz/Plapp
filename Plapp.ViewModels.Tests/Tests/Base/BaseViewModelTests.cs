using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class BaseViewModelTests<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected TViewModel VM;

        protected IFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization { GenerateDelegates = true });

            FreezeFixtures();

            VM = _fixture.Create<TViewModel>();
        }

        protected virtual void FreezeFixtures() { }


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
