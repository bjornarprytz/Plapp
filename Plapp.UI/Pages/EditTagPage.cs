using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.Controls;
using Plapp.UI.Converters;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class EditTagPage : BaseContentPage<IEditViewModel<ITagViewModel>>
    {
        private readonly Button _cancelButton = new();
        private readonly Button _confirmButton = new();
        
        private readonly Entry _keyEntry = new();
        
        private readonly Entry _unitEntry = new();
        private readonly EnumPicker<DataType> _dataTypePicker = new();
        
        public EditTagPage()
        {
            Content = new StackLayout
            {
                Children =
                {
                    _keyEntry,
                    _confirmButton,
                    _cancelButton,
                    _unitEntry,
                    _dataTypePicker,
                }
            };
        }

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.BindCommand(ViewModel, model => model.ConfirmCommand, page => page._confirmButton)
                .DisposeWith(bindingsDisposable);
            this.BindCommand(ViewModel, model => model.CancelCommand, page => page._cancelButton)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, model => model.ToCreate.Unit, page => page._unitEntry.Text);
            
            this.Bind(ViewModel, model => model.ToCreate.DataType, page => page._dataTypePicker.SelectedItem, type => type, viewToVmConverter: ObjectTo.Enum<DataType> )
                .DisposeWith(bindingsDisposable);

            /*
             * 
            this.OneWayBind(ViewModel, model => model.ToCreate.Key, page => _keyEntry.Text)
                .DisposeWith(bindingsDisposable);
             */

        }
    }
}