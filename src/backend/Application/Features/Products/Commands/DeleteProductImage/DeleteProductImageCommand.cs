using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProductImage
{
    public record DeleteProductImageCommand(Guid ProductId, Guid ProductImageId) : IRequest<Result<bool>>;
}
