using Application.Common.Interface;
using Application.DTOs.Responses.Post;
using Application.Features.Posts.Specification;
using AutoMapper;
using Domain.Entities.Posts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Queries.GetPostPublished
{
    public class GetPostPublishedQueryHandler(IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<GetPostPublishedQuery, Result<IEnumerable<PostDTO>>>
    {
        public async Task<Result<IEnumerable<PostDTO>>> Handle(GetPostPublishedQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Post>();
            var specification = new GetPostsPublishedSpecification(request.filter);
            var posts = await repo.GetAllAsync(specification);
            var totalItems = await repo.CountAsync(specification);
            return new PagingResult<IEnumerable<PostDTO>>(mapper.Map<IEnumerable<PostDTO>>(posts), request.filter.PageNumber, request.filter.PageSize, totalItems);
        }
    }
}
