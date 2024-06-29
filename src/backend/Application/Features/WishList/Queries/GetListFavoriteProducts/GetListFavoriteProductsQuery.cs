using Application.DTOs.Filters.WishList;
using Application.DTOs.Responses.Product;
using Domain.Shared;
using MediatR;

namespace Application.Features.WishsList.Queries.GetListFavoriteProducts
{
    public record GetListFavoriteProductsQuery(WishListFilter Filter,Guid UserId) : IRequest<Result<IEnumerable<ProductDTO>>>;
}
