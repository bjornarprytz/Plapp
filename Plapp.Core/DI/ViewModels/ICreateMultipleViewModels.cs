﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ICreateMultipleViewModel<TViewModel> : ITaskViewModel, IIOViewModel
        where TViewModel : IViewModel
    {
        TViewModel Current { get; set; }
        Func<TViewModel> TemplateFactory { get; set; }
        ObservableCollection<TViewModel> Partials { get; set; }
        IEnumerable<TViewModel> GetResult();

        ICommand ConfirmCurrentCommand { get; }
        ICommand BackToPreviousCommand { get; }
    }
}
