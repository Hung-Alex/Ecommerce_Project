using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductImage(int Order, IFormFile Image);
    public record CreateProductSkus(string VariantName
        , string description
        , int Quantity);
    public record CreateProductCommand(string Name
        , string Description
        , string UrlSlug
        , decimal Price
        , string UnitPrice
        , int? Discount
        , Guid BrandId
        , IEnumerable<CreateProductSkus> Variant
        , IEnumerable<CreateProductImage> Images
        ) : IRequest<Result<bool>>, IValidatableRequest;
}
