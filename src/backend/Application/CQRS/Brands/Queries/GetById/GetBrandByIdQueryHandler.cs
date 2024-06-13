using Application.Common.Interface;
using Application.CQRS.Brands.Specification;
using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities.Brands;
using MediatR;

namespace Application.CQRS.Brands.Queries.GetById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDTOs>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BrandDTOs> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var getProductByIdSpecification = new GetBrandByIdSepecification(request.Id);
            var brand = await repo.FindOneAsync(getProductByIdSpecification);
            if (brand == null) return null;
            return _mapper.Map<BrandDTOs>(brand);
        }
    }
}
