using MediatR;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Interactive
{
    public class NavigateAction<TViewModel> : IRequestWrapper
        where TViewModel : IIOViewModel
    {
        public NavigateAction(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; private set; }
    }

    public class NavigateActionHandler<TViewModel> : IHandlerWrapper<NavigateAction<TViewModel>>
        where TViewModel : IIOViewModel
    {
        private readonly INavigator _navigator;

        public NavigateActionHandler(INavigator navigator)
        {
            _navigator = navigator;
        }

        public async Task<Response<Unit>> Handle(NavigateAction<TViewModel> request, CancellationToken cancellationToken)
        {
            await _navigator.GoToAsync(request.ViewModel);

            return Response.Ok();
        }
    }
}
