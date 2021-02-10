using Newtonsoft.Json;
using PCLStorage;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp
{
    public static class FileHelpers
    {
        private static IFileSystem FileSystem => IoC.Get<IFileSystem>();

        public static async Task<T> ReadAsync<T>(string fileName)
        {
            var f = await FileSystem.LocalStorage.GetFileAsync(fileName);

            var s = await f.ReadAllTextAsync();

            return JsonConvert.DeserializeObject<T>(s);
        }

        public static async Task<string> SaveAsync(string desiredName, Stream inputStream)
        {
            var file = await FileSystem.LocalStorage.CreateFileAsync(
                desiredName,
                CreationCollisionOption.GenerateUniqueName);

            using (var stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                await inputStream.CopyToAsync(stream);
            }

            return file.Path;
        }

        public static string PathFromRoot(string fileName)
        {
            return Path.Combine(FileSystem.LocalStorage.Path, fileName);
        }

        public static async Task EnsureDbCreatedAsync(CancellationToken cancellationToken)
        {
            switch (await FileSystem.LocalStorage.CheckExistsAsync("Plapp.db", cancellationToken))
            {
                case ExistenceCheckResult.NotFound:
                    await FileSystem.LocalStorage.CreateFileAsync("Plapp.db", CreationCollisionOption.FailIfExists, cancellationToken);
                    break;
                default:
                    break;
            }
        }
    }
}
