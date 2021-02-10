using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Plapp.iOS.Configuration
{
    public class IOSConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        public IConfigurationStreamProvider Create()
        {
            return new IOSConfigurationStreamProvider();
        }
    }
}