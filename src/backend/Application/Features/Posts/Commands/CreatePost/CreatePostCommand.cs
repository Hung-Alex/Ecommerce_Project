using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Posts.Commands.CreatePost
{
    public record CreatePostCommand(string Title, string ShortDescription, string Description, string UrlSlug, bool Published, IFormFile Image) : IRequest<Result<bool>>, IValidatableRequest;
}
