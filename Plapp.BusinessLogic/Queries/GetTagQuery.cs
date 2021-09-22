using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Plapp.Core;

namespace Plapp.BusinessLogic.Queries
{
    public class GetTagQuery : IRequestWrapper<ITagViewModel>
    {
        public int TagId { get; private set; }

        public GetTagQuery(int tagId)
        {
            TagId = tagId;
        }
    }

    public class GetTagQueryHandler : IHandlerWrapper<GetTagQuery, ITagViewModel>
    {
        private readonly ITagService _tagService;
        private readonly IViewModelFactory _vmFactory;
        private readonly IMapper _mapper;

        public GetTagQueryHandler(
            ITagService tagService,
            IViewModelFactory vmFactory,
            IMapper mapper)
        {
            _tagService = tagService;
            _vmFactory = vmFactory;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper<ITagViewModel>> Handle(GetTagQuery request,
            CancellationToken cancellationToken)
        {
            var tag = await _tagService.FetchAsync(request.TagId, cancellationToken);

            var viewModel = _vmFactory.Create<ITagViewModel>();

            if (tag != default)
                _mapper.Map(tag, viewModel);

            return Response.Ok(viewModel);
        }
    }
}