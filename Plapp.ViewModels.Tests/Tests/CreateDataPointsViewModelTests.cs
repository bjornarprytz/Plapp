using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plapp.Core;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public class CreateDataPointsViewModelTests : BaseCreateMultipleViewModelsTests<CreateDataPointsViewModel, IDataPointViewModel>
    {
        protected override CreateDataPointsViewModel SetUpVM()
        {
            base.SetUpVM();

            return new CreateDataPointsViewModel(prompterMock.Object);
        }
    }
}
