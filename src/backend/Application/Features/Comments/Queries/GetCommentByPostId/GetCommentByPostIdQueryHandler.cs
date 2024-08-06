using Application.Common.Interface;
using Application.DTOs.Responses.Comments;
using Application.Features.Comments.Specification;
using AutoMapper;
using Domain.Entities.Comments;
using Domain.Shared;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentByPostId
{
    public class GetCommentByPostIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCommentByPostIdQuery, Result<IEnumerable<CommentDTO>>>
    {
        public async Task<Result<IEnumerable<CommentDTO>>> Handle(GetCommentByPostIdQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Comment>();
            var specification = new GetCommentsByPostIdSpecification(request.Filter);
            var comments =await repo.GetAllAsync(specification);
            var totalItems = await repo.CountAsync(specification);
            return new PagingResult<IEnumerable<CommentDTO>>(mapper.Map<IEnumerable<CommentDTO>>(comments), request.Filter.PageNumber, request.Filter.PageSize, totalItems);
        }
    }
}
