using Application.Common.Interface;
using Application.DTOs.Responses.Post;
using Application.Features.Posts.Specification;
using AutoMapper;
using Domain.Constants;
using Domain.Entities.Posts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Queries.GetByUrlSlug
{
    public sealed class GetPostByUrlSlugQueryHandler : IRequestHandler<GetPostByUrlSlugQuery, Result<PostDetailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPostByUrlSlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<PostDetailDTO>> Handle(GetPostByUrlSlugQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Post>();
            var post = await repo.FindOneAsync(new GetPostByUrlSlugSpecification(request.UrlSlug));
            if (post is null)
            {
                return Result<PostDetailDTO>.ResultFailures(ErrorConstants.NotFound(request.UrlSlug));
            }
            var postDetailDTO = _mapper.Map<PostDetailDTO>(post);
            return Result<PostDetailDTO>.ResultSuccess(postDetailDTO);
        }
    }
}
