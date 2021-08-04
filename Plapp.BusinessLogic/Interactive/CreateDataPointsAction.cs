using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Interactive
{
    public class CreateDataPointsAction : IRequestWrapper<IEnumerable<IDataPointViewModel>>
    {
        public ITagViewModel Tag { get; }

        public CreateDataPointsAction(ITagViewModel tag)
        {
            Tag = tag;
        }
    }


    public class CreateDataPointsActionHandler : IHandlerWrapper<CreateDataPointsAction, IEnumerable<IDataPointViewModel>>
    {
        private readonly IPrompter _prompter;
        private readonly IViewModelFactory _vmFactory;

        public CreateDataPointsActionHandler(
            IPrompter prompter,
            IViewModelFactory vmFactory
            )
        {
            _prompter = prompter;
            _vmFactory = vmFactory;
        }

        public async Task<IResponseWrapper<IEnumerable<IDataPointViewModel>>> Handle(CreateDataPointsAction request, CancellationToken cancellationToken)
        {
            var dataPoints = await _prompter.CreateMultipleAsync(
                    () => _vmFactory.Create<IDataPointViewModel>(dp => dp.DataType = request.Tag.DataType)
                );

            if (dataPoints.Equals(default) || !dataPoints.Any())
            {
                return Response.Cancel<IEnumerable<IDataPointViewModel>>();
            }

            return Response.Ok(dataPoints);
        }
    }
}
