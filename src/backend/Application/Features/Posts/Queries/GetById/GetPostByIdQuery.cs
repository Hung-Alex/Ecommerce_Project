using Application.DTOs.Responses.Post;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Queries.GetById
{
    public record GetPostByIdQuery(Guid Id) : IRequest<Result<PostDetailDTO>>;
}
