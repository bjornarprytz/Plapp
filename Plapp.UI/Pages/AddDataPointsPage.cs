using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class AddDataPointsPage : BaseContentPage<IEditViewModel<IDataSeriesViewModel>>
    {
        private readonly Button _cancelButton = new();
        private readonly Button _confirmButton = new();
        private readonly Entry _keyEntry = new();
        
        
        public AddDataPointsPage()
        {
            Shell.SetPresentationMode(this, PresentationMode.Modal);
            
            Content = new StackLayout
            {
                Children =
                {
                    _keyEntry,
                    _confirmButton,
                    _cancelButton
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.BindCommand(ViewModel, model => model.ConfirmCommand, page => page._confirmButton)
                .DisposeWith(bindingsDisposable);
            this.BindCommand(ViewModel, model => model.CancelCommand, page => page._cancelButton)
                .DisposeWith(bindingsDisposable);

            /* this throws an exception "Unsupported expression of type 'Constant'. Did you miss the member access prefix in the expression?"
             * 
             
            this.OneWayBind(ViewModel, model => model.ToCreate.Title, page => _keyEntry.Text)
                .DisposeWith(bindingsDisposable);
             */
        }
    }
}