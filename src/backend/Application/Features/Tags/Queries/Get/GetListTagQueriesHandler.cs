using Application.Common.Interface;
using AutoMapper;
using Domain.Shared;
using MediatR;
using Application.DTOs.Responses.Tags;
using Application.Features.Tags.Specification;
using Domain.Entities.Tags;


namespace Application.Features.Tags.Queries.Get
{
    public class GetListTagQueriesHandler : IRequestHandler<GetListTagsQuery, Result<IEnumerable<TagDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetListTagQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<TagDTO>>> Handle(GetListTagsQuery request, CancellationToken cancellationToken)
        {
            var repoTag = _unitOfWork.GetRepository<Tag>();
            var getTagSpecification = new GetTagsSpecification(request.TagFilter);
            var tags = await repoTag.GetAllAsync(getTagSpecification);
            var totalItems = await repoTag.CountAsync(getTagSpecification);
            return new PagingResult<IEnumerable<TagDTO>>(_mapper.Map<IEnumerable<TagDTO>>(tags), request.TagFilter.PageNumber, request.TagFilter.PageSize, totalItems);
        }
    }
}

