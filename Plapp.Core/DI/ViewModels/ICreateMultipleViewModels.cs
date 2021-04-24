using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Plapp.Core
{
    public interface ICreateMultipleViewModel<TViewModel> : ITaskViewModel, IRootViewModel
        where TViewModel : IViewModel
    {
        ObservableCollection<TViewModel> Partials { get; set; }
        IEnumerable<TViewModel> GetResult();
    }
}
