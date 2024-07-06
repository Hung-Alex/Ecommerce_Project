using Application.DTOs.Responses.Post;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Queries.GetByUrlSlug
{
    public record GetPostByUrlSlugQuery(string UrlSlug) : IRequest<Result<PostDetailDTO>>;
}
