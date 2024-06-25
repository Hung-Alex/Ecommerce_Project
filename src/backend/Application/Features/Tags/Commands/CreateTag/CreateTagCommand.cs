using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Tags.Commands.CreateTag
{
    public record CreateTagCommand(string Name, string Description, string UrlSlug) : IRequest<Result<bool>>, IValidatableRequest;
}
