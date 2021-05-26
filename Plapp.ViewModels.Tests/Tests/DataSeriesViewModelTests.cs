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
    public class DataSeriesViewModelTests : BaseViewModelTests<DataSeriesViewModel>
    {
        private Mock<IPrompter> prompterMock;

        protected override void FreezeFixtures()
        {
            base.FreezeFixtures();

            prompterMock = _fixture.Freeze<Mock<IPrompter>>();
        }
        [TestMethod]
        public void AddDataPointCommand_ShouldPromptUser()
        {
            VM.AddDataPointCommand.Execute(null);

            prompterMock.Verify(p => p.CreateMultipleAsync(It.IsAny<Func<IDataPointViewModel>>()), Times.Once);
        }

        [TestMethod]
        public void AddDataPointCommand_PromtProducesDataPoints_DataPointsShouldContainReturnedItemsInOrder()
        {
            var dataPointMock1 = new Mock<IDataPointViewModel>();
            var dataPointMock2 = new Mock<IDataPointViewModel>();

            prompterMock.Setup(p => p.CreateMultipleAsync(It.IsAny<Func<IDataPointViewModel>>()))
                .Returns(Task.FromResult( new List<IDataPointViewModel> { dataPointMock1.Object, dataPointMock2.Object } as IEnumerable<IDataPointViewModel> ));

            VM.AddDataPointCommand.Execute(null);

            VM.DataPoints.Should().ContainInOrder(dataPointMock1.Object, dataPointMock2.Object);
        }

        [TestMethod]
        public void AddDataPointCommand_PromtProducesDataPoints_ShouldInvokePropertyChanged()
        {
            bool dataPointsChanged = false;
            var dataPointMock = new Mock<IDataPointViewModel>();

            prompterMock.Setup(p => p.CreateMultipleAsync(It.IsAny<Func<IDataPointViewModel>>()))
                .Returns(Task.FromResult(new List<IDataPointViewModel> { dataPointMock.Object } as IEnumerable<IDataPointViewModel>));

            VM.ListenToPropertyChanged(nameof(VM.DataPoints), () => dataPointsChanged = true);

            VM.AddDataPointCommand.Execute(null);

            dataPointsChanged.Should().BeTrue();
        }

        [TestMethod]
        public void AddDataPointCommand_PromtReturnsEmpty_ShouldNotInvokePropertyChanged()
        {
            bool dataPointsChanged = false;

            prompterMock.Setup(p => p.CreateMultipleAsync(It.IsAny<Func<IDataPointViewModel>>()))
                .Returns(Task.FromResult(Enumerable.Empty<IDataPointViewModel>()));

            VM.ListenToPropertyChanged(nameof(VM.DataPoints), () => dataPointsChanged = true);

            VM.AddDataPointCommand.Execute(null);

            dataPointsChanged.Should().BeFalse();
        }

        [TestMethod]
        public void AddDataPointCommand_PromtReturnsNull_ShouldNotInvokePropertyChanged()
        {
            bool dataPointsChanged = false;

            prompterMock.Setup(p => p.CreateMultipleAsync(It.IsAny<Func<IDataPointViewModel>>()))
                .Returns(Task.FromResult<IEnumerable<IDataPointViewModel>>(null));

            VM.ListenToPropertyChanged(nameof(VM.DataPoints), () => dataPointsChanged = true);

            VM.AddDataPointCommand.Execute(null);

            dataPointsChanged.Should().BeFalse();
        }
    }
}
