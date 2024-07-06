using Application.Common.Interface;
using Application.DTOs.Responses.Product;
using Application.Features.WishsList.Specification;
using Domain.Entities.WishLists;
using Domain.Shared;
using MediatR;

namespace Application.Features.WishsList.Queries.GetListFavoriteProducts
{
    public sealed class GetListFavoriteProductsQueryHandler : IRequestHandler<GetListFavoriteProductsQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListFavoriteProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetListFavoriteProductsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<WishList>();
            var querySpecification = new GetListFavoriteProductWithImageByUserIdSepecification(request.Filter, request.UserId);
            var wishList = await repo.GetAllAsync(querySpecification);
            var totalItems = await repo.CountAsync(querySpecification);
            return new PagingResult<IEnumerable<ProductDTO>>(
                wishList.Select(x => new ProductDTO()
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Description = x.Product.Description,
                    UnitPrice = x.Product.UnitPrice,
                    Discount = x.Product.Discount,
                    UrlSlug = x.Product.UrlSlug,
                    Price = x.Product.Price,
                    Images = x.Product.Images.Select(x => x.Image.ImageUrl)
                })
                , request.Filter.PageNumber
                , request.Filter.PageSize
                , totalItems);
        }
    }
}
