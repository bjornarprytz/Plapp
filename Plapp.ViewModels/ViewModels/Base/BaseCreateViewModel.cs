using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateViewModel<TViewModel> : BaseTaskViewModel, ICreateViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public TViewModel Partial { get; set; }

        protected BaseCreateViewModel(
            Func<TViewModel> viewModelFactory,
            IPrompter prompter
            ) : base (prompter)
        {
            Partial = viewModelFactory();
        }

        public TViewModel GetResult()
        {
            return IsConfirmed && PartialIsValid()
                ? Partial
                : default;
        }
        protected abstract bool PartialIsValid();
        protected override bool CanConfirm()
        {
            return PartialIsValid();
        }
    }
}
