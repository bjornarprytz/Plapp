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
        protected Mock<IServiceProvider> serviceProviderMock;
        protected TViewModel VM;

        [TestInitialize]
        public void Initialize()
        {
            serviceProviderMock = new Mock<IServiceProvider>();
            VM = SetUpVM();
        }

        protected abstract TViewModel SetUpVM();
    }
}
