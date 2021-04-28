using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ICreateMultipleViewModel<TViewModel> : ITaskViewModel, IRootViewModel
        where TViewModel : IViewModel
    {
        TViewModel Current { get; set; }
        Func<TViewModel> TemplateFunc { get; set; }
        ObservableCollection<TViewModel> Partials { get; set; }
        IEnumerable<TViewModel> GetResult();

        ICommand ConfirmCurrentCommand { get; }
        ICommand BackToPreviousCommand { get; }
    }
}
