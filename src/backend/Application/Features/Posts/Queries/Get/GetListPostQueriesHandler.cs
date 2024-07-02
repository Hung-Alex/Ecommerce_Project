using Application.Common.Interface;
using AutoMapper;
using Domain.Shared;
using MediatR;
using Application.DTOs.Responses.Post;
using Domain.Entities.Posts;
using Application.Features.Posts.Specification;


namespace Application.Features.Posts.Queries.Get
{
    public class GetListPostQueriesHandler : IRequestHandler<GetListPostQuery, Result<IEnumerable<PostDetailDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetListPostQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PostDetailDTO>>> Handle(GetListPostQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Post>();
            var getPostSpecification = new GetPostsSpecification(request.PostFilter);
            var posts = await repo.GetAllAsync(getPostSpecification);
            var totalItems = await repo.CountAsync(getPostSpecification);
            return new PagingResult<IEnumerable<PostDetailDTO>>(_mapper.Map<IEnumerable<PostDetailDTO>>(posts), request.PostFilter.PageNumber, request.PostFilter.PageSize, totalItems);
        }
    }
}

