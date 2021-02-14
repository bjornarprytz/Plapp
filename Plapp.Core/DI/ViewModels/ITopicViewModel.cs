﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITopicViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsSavingTopic { get; }
        bool LacksImage { get; }

        int Id { get; }
        string ImageUri { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        ObservableCollection<IDataSeriesViewModel> DataEntries { get; }

        ICommand OpenTopicCommand { get; }
        ICommand AddImageCommand { get; }
        ICommand AddDataSeriesCommand { get; }


        void AddDataSeries(IDataSeriesViewModel newSeries);
        void AddDataSeries(IEnumerable<IDataSeriesViewModel> newSeries);
    }
}
