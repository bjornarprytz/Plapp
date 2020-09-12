using Plapp.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp
{
    public class DiaryViewModel : BaseViewModel
    {
        public ObservableCollection<ITopicViewModel> Topics { get; private set; }

        public DiaryViewModel()
        {
            SayOkCommand = new CommandHandler(SayOk);
        }

        public ICommand SayOkCommand { get; set; }


        private void SayOk()
        {
            Console.WriteLine("OK");
        }
    }
}
