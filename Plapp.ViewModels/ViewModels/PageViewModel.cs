using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public abstract class PageViewModel : BaseViewModel, IRootViewModel
    {
        protected PageViewModel(IServiceProvider serviceProvider) : base(serviceProvider) { }


        public bool IsShowing { get; private set; }


        public virtual void OnShow()
        {
            IsShowing = true;
        }

        public virtual void OnHide()
        {
            IsShowing = false;
            OnUserInteractionStopped();
        }

        public virtual void OnUserInteractionStopped() { }
    }
}
