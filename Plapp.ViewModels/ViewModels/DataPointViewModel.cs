using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel
    {
        private readonly ILogger _logger;

        public DataPointViewModel(ILogger logger)
        {
            _logger = logger;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }
    }
}
