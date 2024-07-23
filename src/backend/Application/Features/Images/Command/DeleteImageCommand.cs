using Domain.Shared;
using MediatR;

namespace Application.Features.Images.Command
{
    public record DeleteImageCommand(Guid id):IRequest<Result<bool>>;
}
