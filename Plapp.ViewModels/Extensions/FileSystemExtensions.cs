using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public static class FileSystemExtensions
    {
        public static async Task<T> ReadAsync<T>(this string root, string filePath)
        {
            var s = await File.ReadAllTextAsync(Path.Combine(root, filePath));

            return JsonConvert.DeserializeObject<T>(s);
        }

        public static async Task<string> SaveAsync(this string root, string filePath, Stream inputStream)
        {

            var path = Path.Combine(root, filePath);

            using var fileStream = File.OpenWrite(path);
            await inputStream.CopyToAsync(fileStream);

            return path;
        }
    }
}
