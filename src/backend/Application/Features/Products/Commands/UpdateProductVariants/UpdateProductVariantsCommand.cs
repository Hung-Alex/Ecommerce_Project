using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProductVariants
{
    public record UpdateProductVariantsCommand(Guid ProductId, Guid VariantsId, string Name, string Description, int Quantity) : IRequest<Result<bool>>;
}

