using Domain.Shared;
using MediatR;


namespace Application.Features.Slides.Commands.DeleteSlide
{
    public record DeleteSlideCommand(Guid Id) : IRequest<Result<bool>>;
}
