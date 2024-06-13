using Application.Common.Interface;
using Application.CQRS.Brands.Specification;
using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities.Brands;
using Domain.Shared;
using MediatR;


namespace Application.CQRS.Brands.Queries.Get
{
    public class GetListBrandQueriesHandler : IRequestHandler<GetListBrandQuery, Result<IEnumerable<BrandDTOs>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetListBrandQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<BrandDTOs>>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            var brandRepo = _unitOfWork.GetRepository<Brand>();
            var getProductSpecification = new GetBrandsSpecification(request.ProductFilter);
            var brands = await brandRepo.GetAllAsync(getProductSpecification);
            var totalItems = await brandRepo.CountAsync(getProductSpecification);
            return new PagingResult<IEnumerable<BrandDTOs>>(_mapper.Map<IEnumerable<BrandDTOs>>(brands), request.ProductFilter.PageNumber, request.ProductFilter.PageSize, totalItems);
        }
    }
}
