using Application.DTOs.Responses.Tags;
using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Tags.Commands.UpdateTag
{
    public record UpdateTagCommand(Guid Id, string Name, string Description, string UrlSlug) : IRequest<Result<TagDTO>>, IValidatableRequest;
}
