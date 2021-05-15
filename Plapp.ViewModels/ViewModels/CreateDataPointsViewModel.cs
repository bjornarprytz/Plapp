using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class CreateDataPointsViewModel : BaseCreateMultipleViewModel<IDataPointViewModel>
    {
        public CreateDataPointsViewModel(IPrompter prompter) : base(prompter) { }
    }
}
