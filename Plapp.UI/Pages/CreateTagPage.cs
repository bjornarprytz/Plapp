using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class CreateTagPage : BaseContentPage<ICreateViewModel<ITagViewModel>>
    {
        private Button _cancelButton = new();
        private Button _confirmButton = new();
        private Entry _keyEntry = new();
        
        public CreateTagPage()
        {
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

            this.Bind(ViewModel, model => model.ToCreate.Key, page => _keyEntry.Text)
                .DisposeWith(bindingsDisposable);
        }
    }
}