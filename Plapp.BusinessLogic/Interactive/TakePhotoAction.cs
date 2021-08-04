using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Plapp.BusinessLogic.Interactive
{
    public class TakePhotoAction : IRequestWrapper<string>
    {
    }

    public class TakePhotoActionHandler : IHandlerWrapper<TakePhotoAction, string>
    {
        private readonly ICamera _camera;

        public TakePhotoActionHandler(ICamera camera)
        {
            _camera = camera;
        }

        public async Task<IResponseWrapper<string>> Handle(TakePhotoAction request, CancellationToken cancellationToken)
        {
            await using var photoStream = await _camera.TakePhotoAsync();

            if (photoStream == null)
            {
                return Response<string>.Cancel();
            }

            var imageUri = await FileSystem.AppDataDirectory.SaveAsync($"{Guid.NewGuid()}.jpg", photoStream, cancellationToken);

            return Response<string>.Ok(imageUri);
        }
    }
}
