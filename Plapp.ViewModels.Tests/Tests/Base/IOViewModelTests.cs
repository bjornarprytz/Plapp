using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class IOViewModelTests<TViewModel> : BaseViewModelTests<TViewModel>
        where TViewModel : IOViewModel
    {

    }
}
