
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Commands.DeleteSlideImage
{
    public record DeleteSlideImageCommand(Guid SlideId,Guid SlideImageId):IRequest<Result<bool>>;
}
