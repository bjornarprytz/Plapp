using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;
using System;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public class TagViewModelTests : BaseViewModelTests<TagViewModel>
    {
        protected override TagViewModel SetUpVM()
        {
            return new TagViewModel();
        }
    }
}
