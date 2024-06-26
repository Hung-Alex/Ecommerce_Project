using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Commands.UpdateSlide
{
    public record UpdateSlideCommand(Guid Id, string Title, string Description, int Order, bool? Status) : IRequest<Result<bool>>, IValidatableRequest;
}
