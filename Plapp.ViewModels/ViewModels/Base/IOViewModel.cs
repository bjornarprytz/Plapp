using Plapp.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public abstract class IOViewModel : BaseViewModel, IIOViewModel
    {
        public bool IsSavingData { get; private set; }
        public bool IsLoadingData { get; private set; }

        public IAsyncCommand LoadDataCommand { get; private set; }
        public IAsyncCommand SaveDataCommand { get; private set; }

        protected IOViewModel()
        {
            LoadDataCommand = new AsyncCommand(LoadData, allowsMultipleExecutions: false);
            SaveDataCommand = new AsyncCommand(SaveData, allowsMultipleExecutions: false);
        }

        public override void OnShow()
        {
            base.OnShow();

            LoadDataCommand.Execute(null);
        }

        public override void OnUserInteractionStopped() 
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
