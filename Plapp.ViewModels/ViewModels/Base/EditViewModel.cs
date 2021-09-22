using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentValidation.Results;
using Plapp.BusinessLogic;
using Plapp.Core;
using Plapp.ViewModels.Infrastructure;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public abstract class EditViewModel<TViewModel> : BaseViewModel, IEditViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ICompositeValidator<TViewModel> _validators;

        private ReactiveCommand<TViewModel, IEnumerable<ValidationResult>> _validateCommand;

        protected EditViewModel(IViewModelFactory viewModelFactory, ICompositeValidator<TViewModel> validators)
        {
            _viewModelFactory = viewModelFactory;
            _validators = validators;

            /*
             * 
            _validateCommand = ReactiveCommand.CreateFromTask<TViewModel, IEnumerable<ValidationResult>>(ValidateViewModel);

            _validateCommand
                .Subscribe(results =>
                    Error = results.FirstOrDefault(r => !r.IsValid)?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault())
                .DisposeWith(Disposables);

            this.WhenAnyValue(x => x.ToCreate)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .InvokeCommand(_validateCommand)
                .DisposeWith(Disposables);

            var canExecute = this.WhenAnyValue(x => x.Error, err => err.IsMissing());
             */
            
            ConfirmCommand = new PlappCommand(ConfirmAsync);
            CancelCommand = new PlappCommand(CancelAsync);

            ToCreate = _viewModelFactory.Create<TViewModel>();
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        
        [Reactive] public string Error { get; private set; }
        public TViewModel ToCreate { get; protected set; }
        
        private Task<IEnumerable<ValidationResult>> ValidateViewModel(TViewModel viewModel, CancellationToken cancellationToken) => _validators.ValidateAsync(viewModel, cancellationToken);


        private Task CancelAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        private async Task ConfirmAsync()
        {
            await OnConfirm();

            await Shell.Current.GoToAsync("..");
        }
        
        protected abstract Task OnConfirm();
    }
}