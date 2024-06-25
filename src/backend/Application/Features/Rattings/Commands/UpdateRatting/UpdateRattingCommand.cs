using Application.DTOs.Responses.Rattings;
using Application.DTOs.Responses.Tags;
using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Commands.UpdateRatting
{
    public record UpdateRattingCommand(Guid Id, int Rate, string Description,Guid ProductId) : IRequest<Result<RattingDTO>>, IValidatableRequest;
}
