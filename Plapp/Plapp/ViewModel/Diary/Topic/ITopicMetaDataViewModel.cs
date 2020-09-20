using System;
using System.Windows.Input;

namespace Plapp
{
    public interface ITopicMetaDataViewModel : IViewModel
    {
        string ImagePath { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime FirstEntryDate { get; set; }
        DateTime LastEntryDate { get; set; }

        ICommand OpenTopicCommand { get; }
    }
}
