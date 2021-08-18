using System;
using System.Collections.Generic;

namespace Plapp.Core
{
    public interface IGraphViewModel : IViewModel
    {
        string Header { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        ICollection<IDataSeriesViewModel> DataSeries { get; set; }
    }
}
