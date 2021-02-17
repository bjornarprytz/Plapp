using Newtonsoft.Json;
using PCLStorage;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public static class FileSystemExtensions
    {
        public static async Task<T> ReadAsync<T>(this IFileSystem fileSystem, string fileName)
        {
            var f = await fileSystem.LocalStorage.GetFileAsync(fileName);

            var s = await f.ReadAllTextAsync();

            return JsonConvert.DeserializeObject<T>(s);
        }

        public static async Task<string> SaveAsync(this IFileSystem fileSystem, string desiredName, Stream inputStream)
        {
            var file = await fileSystem.LocalStorage.CreateFileAsync(
                desiredName,
                CreationCollisionOption.GenerateUniqueName);

            using (var stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                await inputStream.CopyToAsync(stream);
            }

            return file.Path;
        }

        public static string PathFromRoot(this IFileSystem fileSystem, string fileName)
        {
            return Path.Combine(fileSystem.LocalStorage.Path, fileName);
        }

        
    }
}
