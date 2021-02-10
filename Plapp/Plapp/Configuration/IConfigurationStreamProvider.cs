using System;
using System.IO;
using System.Threading.Tasks;

namespace Plapp
{
    public interface IConfigurationStreamProvider : IDisposable
    {
        Task<Stream> GetStreamAsync();
    }
}
