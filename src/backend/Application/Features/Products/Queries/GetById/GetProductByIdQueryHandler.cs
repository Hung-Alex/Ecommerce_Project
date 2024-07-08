using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Application.DTOs.Responses.Product;
using Domain.Constants;
using Application.Features.Products.Specification;
using Domain.Entities.Products;
using Application.DTOs.Responses.Product.ProductImage;
using Application.DTOs.Responses.Product.Variants;

namespace Application.Features.Products.Queries.GetById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDetailsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ProductDetailsDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Product>();
            var getProductByIdSpecification = new GetProductDetailSpecification(request.Id);
            var product = await repo.FindOneAsync(getProductByIdSpecification);
            if (product == null) return Result<ProductDetailsDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var productDTO = new ProductDetailsDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Discount = product.Discount,
                Price = product.Price,
                UrlSlug = product.UrlSlug,
                Variants = product.ProductSkus.Select(x => new VariantsDTO { Id = x.Id, VariantName = x.Name, Description = x.Description, Quantity = x.Quantity, Price = x.Price }),
                Images = product.Images.Select(x => new ProductImageDTO() { Id = x.Id, Image = x.Image.ImageUrl }),
                CollectionId = product.ProductSubCategories.Select(x => x.Id).ToList()

            };
            return Result<ProductDetailsDTO>.ResultSuccess(productDTO);
        }
    }
}
