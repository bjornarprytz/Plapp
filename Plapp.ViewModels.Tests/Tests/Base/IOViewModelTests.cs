using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plapp.Core;

namespace Plapp.ViewModels.Tests
{
    [TestClass]
    public abstract class IOViewModelTests<TViewModel> : BaseViewModelTests<TViewModel>
        where TViewModel : IOViewModel
    {
        [TestMethod]
        public void OnShow_IsLoadingDataChanged()
        {
            bool IsLoadingDataChanged = false;

            VM.ListenToPropertyChanged(nameof(VM.IsLoadingData), () => IsLoadingDataChanged = true);

            VM.OnShow();

            IsLoadingDataChanged.Should().BeTrue();
        }

        [TestMethod]
        public void OnUserInteractionStopped_IsSavingDataChanged()
        {
            bool IsSavingDataChanged = false;

            VM.ListenToPropertyChanged(nameof(VM.IsSavingData), () => IsSavingDataChanged = true);

            VM.OnUserInteractionStopped();

            IsSavingDataChanged.Should().BeTrue();
        }
    }
}
