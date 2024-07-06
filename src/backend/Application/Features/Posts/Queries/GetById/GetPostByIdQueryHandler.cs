using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.DTOs.Responses.Post;
using Domain.Entities.Posts;
using Application.Features.Posts.Specification;

namespace Application.Features.Posts.Queries.GetById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Result<PostDetailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<PostDetailDTO>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Post>();
            var getPostByIdSpecification = new GetPostByIdSepecification(request.Id);
            var post = await repo.FindOneAsync(getPostByIdSpecification);
            if (post == null) return Result<PostDetailDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var postDTO = _mapper.Map<PostDetailDTO>(post);
            return Result<PostDetailDTO>.ResultSuccess(postDTO);
        }
    }
}
