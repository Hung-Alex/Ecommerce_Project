using Application.DTOs.Filters.Posts;
using Application.DTOs.Responses.Post;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Queries.GetPostPublished
{
    public record GetPostPublishedQuery(PostFilter filter) : IRequest<Result<IEnumerable<PostDTO>>>;
}
