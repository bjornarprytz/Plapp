using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace Plapp.Peripherals
{
    public class Camera : ICamera
    {
        public Camera()
        {

        }
        public async Task<Stream> TakePhotoAsync()
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { });
            
            return photo?.GetStream();
        }
    }
}
