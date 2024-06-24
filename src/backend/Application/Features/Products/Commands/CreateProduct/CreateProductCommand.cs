using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string UrlSlug, decimal Price, string UnitPrice, Guid BrandId,int ? Discount) : IRequest<Result<bool>>, IValidatableRequest;
}
