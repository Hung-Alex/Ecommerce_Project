using Application.Common.Interface;
using Domain.Shared;
using MediatR;
using Application.Features.Products.Specification;
using Domain.Entities.Products;
using Application.DTOs.Responses.Product;


namespace Application.Features.Products.Queries.Get
{
    public class GetListProductQueriesHandler : IRequestHandler<GetListProductQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListProductQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.GetRepository<Product>();
            var getProductSpecification = new GetProductsSpecification(request.ProductFilter);
            var products = await productRepo.GetAllAsync(getProductSpecification);
            var totalItems = await productRepo.CountAsync(getProductSpecification);
            return new PagingResult<IEnumerable<ProductDTO>>(products.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                Discount = x.Discount,
                BrandId = x.BrandId,
                Price = x.Price,
                Images = x.Images.Select(x => x.Image.ImageUrl)
            })
                , request.ProductFilter.PageNumber
                , request.ProductFilter.PageSize
                , totalItems);
        }
    }
}
