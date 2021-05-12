using Plapp.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public abstract class PageViewModel : BaseViewModel, IRootViewModel
    {
        public bool IsSavingData { get; private set; }
        public bool IsLoadingData { get; private set; }
        public bool IsShowing { get; private set; }

        public ICommand LoadDataCommand { get; private set; }

        public ICommand SaveDataCommand { get; private set; }

        protected PageViewModel()
        {
            LoadDataCommand = new AsyncCommand(LoadData, allowsMultipleExecutions: false);
            SaveDataCommand = new AsyncCommand(SaveData, allowsMultipleExecutions: false);
        }

        public virtual void OnShow()
        {
            IsShowing = true;

            LoadDataCommand.Execute(null);
        }

        public virtual void OnHide()
        {
            IsShowing = false;
            OnUserInteractionStopped();
        }

        public virtual void OnUserInteractionStopped() 
        {
            SaveDataCommand.Execute(null);
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
