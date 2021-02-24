using Plapp.Core;
using System;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateViewModel<TViewModel> : BaseViewModel, ICreateViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public abstract IViewModel UnderCreation { get; }

        protected BaseCreateViewModel(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public abstract Task<TViewModel> Creation();
    }
}
