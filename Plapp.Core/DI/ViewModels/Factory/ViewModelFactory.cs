using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp.Core
{
    public delegate TViewModel ViewModelFactory<out TViewModel>() where TViewModel : IViewModel;
}
