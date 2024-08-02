using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.Features.Products.Specification;
using Domain.Entities.Products;
using Application.DTOs.Responses.Product.Admin;
using Application.DTOs.Responses.Images;

namespace Application.Features.Products.Queries.GetById
{
    public sealed class GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductByIdQuery, Result<ProductDetailAdminDTO>>
    {
        public async Task<Result<ProductDetailAdminDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var getProductByIdSpecification = new GetProductDetailSpecification(request.Id);
            var product = await repo.FindOneAsync(getProductByIdSpecification);
            if (product == null) return Result<ProductDetailAdminDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var productDTO = new ProductDetailAdminDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                IsStock = product.IsStock,
                OldPrice = product.OldPrice,
                UrlSlug = product.UrlSlug,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Images = mapper.Map<IEnumerable<ImageDTO>>(product.Images),
            };
            return Result<ProductDetailAdminDTO>.ResultSuccess(productDTO);
        }
    }
}
