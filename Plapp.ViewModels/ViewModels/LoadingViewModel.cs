using System;
using Plapp.Core;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public class LoadingViewModel : BaseViewModel, ILoadingViewModel
    {
        public string Animation { get; private set; }

        public LoadingViewModel()
        {
            Animation = Path.Combine("Animations","Pineapple.json");
        }
    }
}
