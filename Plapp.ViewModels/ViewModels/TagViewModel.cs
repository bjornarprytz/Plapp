using Plapp.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class TagViewModel : BaseViewModel, ITagViewModel
    {
        public TagViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            SaveCommand = new AsyncCommand(SaveAsync, allowsMultipleExecutions: false);
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public string Unit { get; set; }
        public DataType DataType { get; set; }
        public Icon Icon { get; set; }
        public string Color { get; set; }

        public ICommand SaveCommand { get; private set; }

        internal void Hydrate(Tag tagData)
        {
            Id = tagData.Id;
            Key = tagData.Key;
            Unit = tagData.Unit;
            Color = tagData.Color;
            DataType = tagData.DataType;
            Icon = tagData.Icon;
        }

        private async Task SaveAsync()
        {
            await DataStore.SaveTagAsync(this.ToModel());
        }
    }
}
