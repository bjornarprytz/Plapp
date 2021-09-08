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
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public abstract class CreateViewModel<TViewModel> : BaseViewModel, ICreateViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ICompositeValidator<TViewModel> _validators;

        private ReactiveCommand<TViewModel, IEnumerable<ValidationResult>> _validateCommand;

        protected CreateViewModel(IViewModelFactory viewModelFactory, ICompositeValidator<TViewModel> validators)
        {
            _viewModelFactory = viewModelFactory;
            _validators = validators;
            
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
            
            ConfirmCommand = ReactiveCommand.CreateFromTask(SaveViewModelAsync, canExecute);
            CancelCommand = ReactiveCommand.CreateFromTask(GoBackAsync);
        }

        [Reactive] public ICommand ConfirmCommand { get; private set; }
        public ICommand CancelCommand { get; }
        [Reactive] public string Error { get; private set; }
        public TViewModel ToCreate { get; }
        
        private Task<IEnumerable<ValidationResult>> ValidateViewModel(TViewModel viewModel, CancellationToken cancellationToken) => _validators.ValidateAsync(viewModel, cancellationToken);

        private Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
        
        protected abstract Task SaveViewModelAsync();
    }
}