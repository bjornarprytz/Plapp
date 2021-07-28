using AutoMapper;
using MediatR;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class SaveTagCommand : IRequestWrapper
    {
        public SaveTagCommand(ITagViewModel tag)
        {
            Tag = tag;
        }
        public ITagViewModel Tag { get; private set; }
    }

    public class SaveTagCommandHandler : IHandlerWrapper<SaveTagCommand>
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public SaveTagCommandHandler(
            ITagService tagService,
            IMapper mapper
            )
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(SaveTagCommand request, CancellationToken cancellationToken)
        {
            var tag = _mapper.Map<Tag>(request.Tag);

            await _tagService.SaveAsync(tag, cancellationToken);

            request.Tag.Id = tag.Id;

            return Response.Ok();
        }
    }
}
