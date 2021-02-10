using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly SemaphoreSlim semaphoreSlim;
        private readonly IConfigurationStreamProviderFactory factory;
        private bool initialized;

        private Configuration configuration;

        public ConfigurationManager(IConfigurationStreamProviderFactory configurationStreamProviderFactory)
        {
            semaphoreSlim = new SemaphoreSlim(1, 1);

            factory = configurationStreamProviderFactory;
        }

        public async Task<Configuration> GetAsync(CancellationToken cancellationToken)
        {
            await InitializeAsync(cancellationToken);

            if (configuration == null)
                throw new InvalidOperationException("Failed to find configuration file. Is it present on the platform?");

            return configuration;
        }

        public async Task<Configuration> GetAsync()
        {
            await InitializeAsync(default);

            if (configuration == null)
                throw new InvalidOperationException("Failed to find configuration file. Is it present on the platform?");

            return configuration;
        }

        private async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (initialized)
            {
                return;
            }

            try
            {
                await semaphoreSlim.WaitAsync(cancellationToken);

                configuration = await ReadAsync();

                initialized = true;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task<Configuration> ReadAsync()
        {
            using var streamProvider = factory.Create();
            using var stream = await streamProvider.GetStreamAsync();

            return Deserialize<Configuration>(stream);
        }

        private T Deserialize<T>(Stream stream)
        {
            if (stream == null || !stream.CanRead)
            {
                return default;
            }

            using var sr = new StreamReader(stream);
            using var jtr = new JsonTextReader(sr);
            
            return new JsonSerializer().Deserialize<T>(jtr);            
        }
    }
}
