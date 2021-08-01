using AutoMapper;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Queries
{
    public class GetAllTagsQuery : IRequestWrapper<IEnumerable<ITagViewModel>>
    {
    }

    public class GetAllTagsQueryHandler : IHandlerWrapper<GetAllTagsQuery, IEnumerable<ITagViewModel>>
    {
        private readonly ITagService _tagService;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IMapper _mapper;

        public GetAllTagsQueryHandler(
            ITagService tagService,
            IViewModelFactory viewModelFactory,
            IMapper mapper)
        {
            _tagService = tagService;
            _viewModelFactory = viewModelFactory;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ITagViewModel>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _tagService.FetchAllAsync(cancellationToken);

            var viewModels = tags.Select(t => _mapper.Map(t, _viewModelFactory.Create<ITagViewModel>()));

            return Response.Ok(viewModels);
        }
    }
}
