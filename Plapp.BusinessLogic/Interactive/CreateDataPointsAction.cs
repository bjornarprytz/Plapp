using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Interactive
{
    public class CreateDataPointsAction : IRequestWrapper<IEnumerable<IDataPointViewModel>>
    {
    }


    public class CreateDataPointsActionHandler : IHandlerWrapper<CreateDataPointsAction, IEnumerable<IDataPointViewModel>>
    {
        private readonly IPrompter _prompter;
        private readonly ViewModelFactory<IDataPointViewModel> _viewModelFactory;

        public CreateDataPointsActionHandler(
            IPrompter prompter,
            ViewModelFactory<IDataPointViewModel> viewModelFactory
            )
        {
            _prompter = prompter;
            _viewModelFactory = viewModelFactory;
        }

        public async Task<Response<IEnumerable<IDataPointViewModel>>> Handle(CreateDataPointsAction request, CancellationToken cancellationToken)
        {
            var dataPoints = await _prompter.CreateMultipleAsync(
                    () => _viewModelFactory() // TODO: Make different DataPoints depending on Tag.DataType
                );

            if (dataPoints == default || !dataPoints.Any())
            {
                return Response.Cancel<IEnumerable<IDataPointViewModel>>();
            }

            return Response.Ok(dataPoints);
        }
    }
}
