using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductSkus(string VariantName
        , string Description
        , int Quantity);
    public record AddCategories(Guid ParrentId, Guid? SubCategoryId);
    public record CreateProductCommand(string Name
        , string Description
        , string UrlSlug
        , decimal Price
        , string UnitPrice
        , int? Discount
        , Guid BrandId
        , IEnumerable<CreateProductSkus> Variant
        , IEnumerable<AddCategories> Collections
        , IFormFileCollection? Images
        ) : IRequest<Result<bool>>, IValidatableRequest;
}
