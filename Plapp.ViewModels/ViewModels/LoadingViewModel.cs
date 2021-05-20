using System;
using System.IO;

namespace Plapp.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        public string Animation { get; private set; }

        public LoadingViewModel()
        {
            
            Animation = Path.Combine("Animations","Pineapple.json");
        }
    }
}
