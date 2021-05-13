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
    public class CreateTagViewModelTests : BaseCreateViewModelTests<CreateTagViewModel, ITagViewModel>
    {
        // TODO: Test OnShow(), that LoadAvailableTags is called

        protected override CreateTagViewModel SetUpVM()
        {
            base.SetUpVM();

            var tagServiceMock = new Mock<ITagService>();

            return new CreateTagViewModel(
                factoryMock.Object,
                prompterMock.Object,
                tagServiceMock.Object
                );
        }
    }
}
