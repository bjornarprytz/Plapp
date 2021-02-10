using System;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.iOS.Configuration
{
    public class IOSConfigurationStreamProvider : IConfigurationStreamProvider
    {
        private const string ConfigurationFilePath = "Assets/appsettings.json";

        private Stream _readingStream;

        public Task<Stream> GetStreamAsync()
        {
            Dispose(false);

            _readingStream = new FileStream(ConfigurationFilePath, FileMode.Open, FileAccess.Read);

            return Task.FromResult(_readingStream);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~IOSConfigurationStreamProvider()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            _readingStream?.Dispose();
            _readingStream = null;
        }
    }

}