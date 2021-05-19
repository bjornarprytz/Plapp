using AutoFixture;
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
    public abstract class BaseCreateViewModelTests<TViewModel, TTemplate> : BaseTaskViewModelTests<TViewModel>
        where TViewModel : BaseCreateViewModel<TTemplate>
        where TTemplate : class, IViewModel
    {
        protected Mock<ViewModelFactory<TTemplate>> factoryMock;
        protected Mock<TTemplate> templateMock;

        protected override void FreezeFixtures()
        {
            base.FreezeFixtures();

            templateMock = _fixture.Freeze<Mock<TTemplate>>();
            factoryMock = _fixture.Freeze<Mock<ViewModelFactory<TTemplate>>>();

            factoryMock.Setup(f => f()).Returns(templateMock.Object);
        }

        [TestMethod]
        public virtual void Ctor_PartialIsFromFactory()
        {
            VM.Partial.Should().Be(templateMock.Object);
        }

        [TestMethod]
        public virtual void ConfirmCommand_GetResultShouldBeThePartial()
        {
            var vmMock = new Mock<TTemplate>();

            VM.Partial = vmMock.Object;

            VM.ConfirmCommand.Execute(null);

            VM.GetResult().Should().Be(vmMock.Object);
        }
    }
}
