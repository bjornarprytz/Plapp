using Plapp.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateViewModel<TViewModel> : BaseViewModel, ICreateViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public abstract IViewModel UnderCreation { get; set; }

        public ICommand ConfirmCommand => throw new NotImplementedException(); // TODO: How to enable consumers to await this? Events?

        public ICommand CancelCommand => throw new NotImplementedException(); // TODO: How to enable consumers to await this? Events?

        protected BaseCreateViewModel(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public abstract Task<TViewModel> Creation();
    }
}
