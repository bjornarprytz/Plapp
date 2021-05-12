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

        [TestMethod]
        public void Hydrate_PopulatesAllFields()
        {
            var tag = new Tag
            {
                Id = 1,
                Icon = Icon.Diamond,
                Color = "some color",
                DataType = DataType.Decimal,
                Key = "some key",
                Unit = "some unit",
            };

            VM.Hydrate(tag);

            VM.Should().BeEquivalentTo(
                new TagViewModel
                {
                    Id = 1,
                    Icon = Icon.Diamond,
                    Color = "some color",
                    DataType = DataType.Decimal,
                    Key = "some key",
                    Unit = "some unit"
                });
        }
    }
}
