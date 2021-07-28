using Plapp.Core;
using System.IO;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public class LoadingViewModel : IOViewModel, ILoadingViewModel
    {
        public string Animation { get; private set; }

        public LoadingViewModel()
        {
            
            Animation = Path.Combine("Animations","Pineapple.json");
        }
    }
}
