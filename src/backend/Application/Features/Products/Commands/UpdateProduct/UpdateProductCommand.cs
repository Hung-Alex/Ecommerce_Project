using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string UrlSlug, Guid BrandId, Guid CategoryId, decimal Price, int? Discount, Decimal? OldPrice,bool IsStock) : IRequest<Result<bool>>, IValidatableRequest;
}
