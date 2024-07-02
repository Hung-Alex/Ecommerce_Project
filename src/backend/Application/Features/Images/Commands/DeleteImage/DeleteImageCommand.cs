using Domain.Shared;
using MediatR;

namespace Application.Features.Images.Commands.DeleteImage
{
    public record DeleteImageCommand(Guid Id) : IRequest<Result<bool>>;
}
