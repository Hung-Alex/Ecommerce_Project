using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Slides.Commands.AddProductSlide
{
    public record AddSlideImageCommand(Guid SlideId, IFormFile File) : IRequest<Result<bool>>;
}
