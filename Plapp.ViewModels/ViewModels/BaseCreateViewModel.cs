using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateViewModel<TViewModel> : BaseTaskViewModel, ICreateViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public TViewModel Result { get; set; }

        protected BaseCreateViewModel(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
