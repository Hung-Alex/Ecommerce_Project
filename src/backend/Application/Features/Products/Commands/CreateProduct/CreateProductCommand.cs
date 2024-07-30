using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name
        , string Description
        , string UrlSlug
        , decimal Price
        , Decimal? OldPrice
        , int? Discount
        , Guid BrandId
        , Guid CategoryId
        , bool IsStock
        , IFormFileCollection? Images
        ) : IRequest<Result<bool>>, IValidatableRequest;
}
