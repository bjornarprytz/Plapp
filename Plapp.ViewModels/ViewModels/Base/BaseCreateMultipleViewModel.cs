using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateMultipleViewModel<TViewModel> : BaseTaskViewModel, ICreateMultipleViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public ObservableCollection<TViewModel> Partials { get; set; }
        public TViewModel Current { get; set; }
        public Func<TViewModel> TemplateFactory { get; set; }

        public ICommand ConfirmCurrentCommand { get; private set; }
        public ICommand BackToPreviousCommand { get; private set; }

        protected BaseCreateMultipleViewModel(IPrompter prompter) : base(prompter)
        {
            Partials = new ObservableCollection<TViewModel>();

            ConfirmCurrentCommand = new Command(ConfirmCurrent);
            BackToPreviousCommand = new Command(BackToPrevious, () => Partials.Count > 0);
        }

        public IEnumerable<TViewModel> GetResult()
        {
            return IsConfirmed ?
                Partials
                : Enumerable.Empty<TViewModel>();
        }

        protected override bool CanConfirm()
        {
            return Partials.Count > 0 || CurrentIsValid();
        }

        protected override void OnConfirm()
        {
            base.OnConfirm();

            if (CurrentIsValid())
            {
                Partials.Add(Current);
            }
        }

        protected virtual void OnConfirmCurrent() { }

        private void ConfirmCurrent()
        {
            OnConfirmCurrent();

            Partials.Add(Current);

            Current = (TemplateFactory != null) ? TemplateFactory() : default;

            (BackToPreviousCommand as Command).ChangeCanExecute();
        }

        private void BackToPrevious()
        {
            Current = Partials.LastOrDefault();

            if (Current is not null)
                Partials.Remove(Current);
            else
                Current = (TemplateFactory != null) ? TemplateFactory() : default;

            (BackToPreviousCommand as Command).ChangeCanExecute();
        }

        private bool CurrentIsValid()
        {
            return Current != null; // Should this also check for extra data validation? If so, update CreateTag as well probably.
        }
    }
}
