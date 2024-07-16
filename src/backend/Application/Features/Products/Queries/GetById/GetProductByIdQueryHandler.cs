using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.Features.Products.Specification;
using Domain.Entities.Products;

using Application.DTOs.Responses.Product.Admin;

namespace Application.Features.Products.Queries.GetById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDetailAdminDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ProductDetailAdminDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Product>();
            var getProductByIdSpecification = new GetProductDetailSpecification(request.Id);
            var product = await repo.FindOneAsync(getProductByIdSpecification);
            if (product == null) return Result<ProductDetailAdminDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var productDTO = new ProductDetailAdminDTO()
            {
               
            };
            return Result<ProductDetailAdminDTO>.ResultSuccess(productDTO);
        }
    }
}
