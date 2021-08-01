using MediatR;
using Plapp.BusinessLogic.Commands;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Interactive
{
    public class AddDataSeriesAction : IRequestWrapper<IDataSeriesViewModel>
    {
        public AddDataSeriesAction(ITopicViewModel parentTopic)
        {
            Topic = parentTopic;
        }

        public ITopicViewModel Topic { get; private set; }
    }

    public class AddDataSeriesActionHandler : IHandlerWrapper<AddDataSeriesAction, IDataSeriesViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IViewModelFactory _vmFactory;

        public AddDataSeriesActionHandler(
            IMediator mediator,
            IViewModelFactory vmFactory
            )
        {
            _mediator = mediator;
            _vmFactory = vmFactory;
        }

        public async Task<Response<IDataSeriesViewModel>> Handle(AddDataSeriesAction request, CancellationToken cancellationToken)
        {
            var chooseResult = await _mediator.Send(new PickTagAction());

            if (chooseResult.Error)
                chooseResult.Throw();

            var chosenTag = chooseResult.Data;

            if (chosenTag == null) return Response.Cancel<IDataSeriesViewModel>();

            var newDataSeries = _vmFactory.Create<IDataSeriesViewModel>();

            newDataSeries.Topic = request.Topic;
            newDataSeries.Tag = chosenTag;

            request.Topic.DataSeries.Add(newDataSeries);

            var saveResult = await _mediator.Send(new SaveDataSeriesCommand(newDataSeries));

            if (saveResult.Error)
                saveResult.Throw();

            return Response.Ok(newDataSeries);
        }
    }
}
