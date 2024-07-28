using Application.DTOs.Filters.Comments;
using Application.DTOs.Responses.Comments;
using Domain.Shared;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentByPostId
{
    public record GetCommentByPostIdQuery(CommentFilter Filter) : IRequest<Result<IEnumerable<CommentDTO>>>;
}
