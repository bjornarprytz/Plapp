using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public abstract class BaseCreateMultipleViewModel<TViewModel> : BaseTaskViewModel, ICreateMultipleViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public ObservableCollection<TViewModel> Partials { get; set; }
        public TViewModel Current { get; set; }

        public ICommand ConfirmCurrentCommand { get; private set; }

        protected BaseCreateMultipleViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Partials = new ObservableCollection<TViewModel>();
            Current = ServiceProvider.Get<TViewModel>();

            ConfirmCurrentCommand = new CommandHandler(ConfirmCurrent);
        }

        public IEnumerable<TViewModel> GetResult()
        {
            return IsConfirmed ?
                Partials
                : Enumerable.Empty<TViewModel>();
        }

        protected override bool CanConfirm()
        {
            return Partials.Count > 0; // Should this also check for data validation?
        }

        private void ConfirmCurrent()
        {
            Partials.Add(Current);

            Current = ServiceProvider.Get<TViewModel>();
        }
    }
}
