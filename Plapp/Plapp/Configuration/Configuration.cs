using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Plapp
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string PlappDb { get; set; }
    }

}
