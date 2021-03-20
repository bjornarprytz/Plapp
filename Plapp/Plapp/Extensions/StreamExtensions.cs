using Newtonsoft.Json;
using System.IO;

namespace Plapp
{
    public static class StreamExtensions
    {

        public static T Deserialize<T>(this Stream stream)
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
