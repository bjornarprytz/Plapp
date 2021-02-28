using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateViewModel<TViewModel> : BaseTaskViewModel, ICreateViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public TViewModel Partial { get; set; }

        protected BaseCreateViewModel(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            Partial = ServiceProvider.Get<TViewModel>();
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
