using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProductVariants
{
    public record DeleteProductVariantsCommand(Guid ProductId, Guid VariantId) : IRequest<Result<bool>>;
}
