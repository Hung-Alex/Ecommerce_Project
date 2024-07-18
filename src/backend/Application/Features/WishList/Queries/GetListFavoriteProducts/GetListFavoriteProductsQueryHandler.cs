using Application.Common.Interface;
using Application.DTOs.Responses.Product.Client;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.Features.WishsList.Specification;
using AutoMapper;
using Domain.Entities.WishLists;
using Domain.Shared;
using MediatR;

namespace Application.Features.WishsList.Queries.GetListFavoriteProducts
{
    public sealed class GetListFavoriteProductsQueryHandler : IRequestHandler<GetListFavoriteProductsQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetListFavoriteProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                    UrlSlug = x.Product.UrlSlug,
                    Discount = x.Product.Discount,
                    Price = x.Product.Price,
                    Brand = _mapper.Map<BrandProductDTO>(x.Product.Brand),
                    Category = _mapper.Map<CategoryProductDTO>(x.Product.Category),
                    Rate = x.Product.Rattings.Count() > 0 ? x.Product.Rattings.Average(r => r.Rate) : 0,
                    TotalRate = x.Product.Rattings.Count(),
                    Images = x.Product.Images.Select(p => p.ImageUrl).ToList(),
                })
                , request.Filter.PageNumber
                , request.Filter.PageSize
                , totalItems);
        }
    }
}
