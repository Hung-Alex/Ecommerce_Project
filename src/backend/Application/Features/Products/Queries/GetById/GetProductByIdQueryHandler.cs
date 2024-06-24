using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Application.DTOs.Responses.Product;
using Domain.Constants;
using Application.Features.Products.Specification;
using Domain.Entities.Products;

namespace Application.Features.Products.Queries.GetById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Product>();
            var getProductByIdSpecification = new GetProductByIdSepecification(request.Id);
            var product = await repo.FindOneAsync(getProductByIdSpecification);
            if (product == null) return Result<ProductDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.ResultSuccess(productDTO);
        }
    }
}
