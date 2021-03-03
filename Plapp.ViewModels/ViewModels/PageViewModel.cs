using Plapp.Core;
using System;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public abstract class PageViewModel : BaseViewModel, IRootViewModel
    {
        protected PageViewModel(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public bool IsSavingData { get; private set; }
        public bool IsLoadingData { get; private set; }
        public bool IsShowing { get; private set; }


        public virtual void OnShow()
        {
            IsShowing = true;

            Task.Run(LoadData);
        }

        public virtual void OnHide()
        {
            IsShowing = false;
            OnUserInteractionStopped();
        }

        public virtual void OnUserInteractionStopped() 
        {
            Task.Run(SaveData);
        }

        private Task SaveData()
        {
            return FlagActionAsync(
                () => IsSavingData,
                AutoSaveDataAsync);
        }

        private Task LoadData()
        {
            return FlagActionAsync(
                () => IsLoadingData,
                AutoLoadDataAsync);
        }


        protected virtual async Task AutoLoadDataAsync() { }
        protected virtual async Task AutoSaveDataAsync() { }
    }
}
