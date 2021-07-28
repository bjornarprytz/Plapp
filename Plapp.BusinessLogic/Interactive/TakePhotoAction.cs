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

        public async Task<Response<string>> Handle(TakePhotoAction request, CancellationToken cancellationToken)
        {
            using var photoStream = await _camera.TakePhotoAsync();

            if (photoStream == null)
            {
                return Response.Ok<string>(string.Empty);
            }

            var imageUri = await FileSystem.AppDataDirectory.SaveAsync($"{Guid.NewGuid()}.jpg", photoStream);

            return Response.Ok<string>(imageUri);
        }
    }
}
