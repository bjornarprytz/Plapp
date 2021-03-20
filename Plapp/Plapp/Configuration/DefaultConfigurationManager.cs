using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Plapp
{
    public class DefaultConfigurationManager : IConfigurationManager
    {
        readonly string _filename;
        public DefaultConfigurationManager(string filename)
        {
            _filename = filename;
        }

        public async Task<Configuration> GetAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(_filename);

            return stream.Deserialize<Configuration>();
        }
    }
}
