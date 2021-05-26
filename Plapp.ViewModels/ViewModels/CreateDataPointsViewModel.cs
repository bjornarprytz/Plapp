using Plapp.Core;
using System;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public class CreateDataPointsViewModel : BaseCreateMultipleViewModel<IDataPointViewModel>
    {
        public CreateDataPointsViewModel(IPrompter prompter) : base(prompter) { }

        protected override void OnConfirmCurrent()
        {
            base.OnConfirmCurrent();

            Current.Date = DateTime.Now;
        }
    }
}
