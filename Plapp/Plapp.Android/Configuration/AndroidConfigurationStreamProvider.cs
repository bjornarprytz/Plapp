using Android.Content;
using Android.Content.Res;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.Droid.Configuration
{
    public class AndroidConfigurationStreamProvider : IConfigurationStreamProvider
    {
        private const string ConfigurationFilePath = "appsettings.json";

        private readonly Func<Context> contextProvider;

        private Stream readingStream;

        public AndroidConfigurationStreamProvider(Func<Context> contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public Task<Stream> GetStreamAsync()
        {
            Dispose(false);

            AssetManager assets = contextProvider().Assets;

            readingStream = assets.Open(ConfigurationFilePath);

            return Task.FromResult(readingStream);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AndroidConfigurationStreamProvider()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            readingStream?.Dispose();
            readingStream = null;
        }
    }
}