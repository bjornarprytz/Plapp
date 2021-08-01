using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public static class FileSystemExtensions
    {
        public static async Task<T> ReadAsync<T>(this string root, string filePath, CancellationToken cancellationToken)
        {
            var s = await File.ReadAllTextAsync(Path.Combine(root, filePath), cancellationToken);

            return JsonConvert.DeserializeObject<T>(s);
        }

        public static async Task<string> SaveAsync(this string root, string filePath, Stream inputStream, CancellationToken cancellationToken)
        {

            var path = Path.Combine(root, filePath);

            using var fileStream = File.OpenWrite(path);
            await inputStream.CopyToAsync(fileStream, cancellationToken);

            return path;
        }
    }
}
