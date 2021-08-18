using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plapp.BusinessLogic.Interactive
{
    public class NavigateAction : IRequestWrapper
    {
        public string Route { get; }

        public NavigateAction(string route)
        {
            Route = route;
        }
    }

    public class NavigateActionHandler<TViewModel> : IHandlerWrapper<NavigateAction>
        where TViewModel : class, IViewModel
    {

        public async Task<IResponseWrapper> Handle(NavigateAction request, CancellationToken cancellationToken)
        {
            await Shell.Current.GoToAsync(request.Route);

            return Response.Ok();
        }
    }
}
