using System.IO;
using System.Threading.Tasks;

namespace Plapp
{
    public interface ICamera
    {
        Task<Stream> TakePhotoAsync();
    }
}
