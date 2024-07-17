using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string UrlSlug, Guid BrandId, Guid CategoryId, decimal Price, int? Discount) : IRequest<Result<bool>>, IValidatableRequest;
}
