using MediatR;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Interactive
{
    public class NavigateAction<TViewModel> : IRequestWrapper
        where TViewModel : IIOViewModel
    {
        // This does not work because requests/handlers can't have generic type input
        // TODO: Refactor the INavigator to use an enum instead.

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

        public async Task<IResponseWrapper> Handle(NavigateAction<TViewModel> request, CancellationToken cancellationToken)
        {
            await _navigator.GoToAsync(request.ViewModel);

            return Response.Ok();
        }
    }
}
