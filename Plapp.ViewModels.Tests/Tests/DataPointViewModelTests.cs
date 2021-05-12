
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public class DataPointViewModelTests : BaseViewModelTests<DataPointViewModel>
    {
        private Mock<ILogger> loggerMock;

        protected override DataPointViewModel SetUpVM()
        {
            loggerMock = new Mock<ILogger>();

            return new DataPointViewModel(loggerMock.Object);
        }
    }
}
