using Application.DTOs.Filters.Posts;
using Application.DTOs.Responses.Post;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Queries.Get
{
    public record GetListPostQuery(PostFilter PostFilter) : IRequest<Result<IEnumerable<PostDTO>>>;
}
