using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Commands.CreateSlide
{
    public record CreateSlideCommand(string Title, string Description, int Order, bool? Status) : IRequest<Result<bool>>, IValidatableRequest;
}
