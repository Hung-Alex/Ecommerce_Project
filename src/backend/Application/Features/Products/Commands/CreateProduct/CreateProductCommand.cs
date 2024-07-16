using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductSkus(string VariantName
        , string Description);
    public record CreateProductCommand(string Name
        , string Description
        , string UrlSlug
        , decimal Price
        , int? Discount
        , Guid BrandId
        , Guid CategoryId
        , IEnumerable<CreateProductSkus> Variant
        , IFormFileCollection? Images
        ) : IRequest<Result<bool>>, IValidatableRequest;
}
