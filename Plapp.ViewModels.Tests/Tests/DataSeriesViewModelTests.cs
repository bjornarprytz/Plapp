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
        private Mock<IDataSeriesService> dataSeriesServiceMock;
        private Mock<ViewModelFactory<IDataPointViewModel>> dataPointFactoryMock;
        private Mock<ILogger> loggerMock;

        protected override DataSeriesViewModel SetUpVM()
        {
            prompterMock = new Mock<IPrompter>();
            dataSeriesServiceMock = new Mock<IDataSeriesService>();
            dataPointFactoryMock = new Mock<ViewModelFactory<IDataPointViewModel>>();
            loggerMock = new Mock<ILogger>();

            return new DataSeriesViewModel(
                prompterMock.Object,
                dataSeriesServiceMock.Object,
                dataPointFactoryMock.Object,
                loggerMock.Object,
                mapper
                );
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

        // TODO: Test RefreshCommand (rename to LoadCommand?) and SaveCommand
    }
}
