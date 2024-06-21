using Application.Common.Interface;
using Application.Features.Brands.Specification;
using Application.DTOs.Responses.Brand;
using AutoMapper;
using Domain.Entities.Brands;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BrandDTOs>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BrandDTOs> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var getProductByIdSpecification = new GetBrandByIdSepecification(request.Id);
            var brand = await repo.FindOneAsync(getProductByIdSpecification);
            if (brand == null) return null;
            return _mapper.Map<BrandDTOs>(brand);
        }
    }
}
