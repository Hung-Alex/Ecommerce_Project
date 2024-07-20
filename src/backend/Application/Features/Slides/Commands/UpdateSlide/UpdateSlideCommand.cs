using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Slides.Commands.UpdateSlide
{
    public record UpdateSlideCommand(Guid Id, string Title, string Description, bool IsActive, IFormFile? Image) : IRequest<Result<bool>>, IValidatableRequest;
}
