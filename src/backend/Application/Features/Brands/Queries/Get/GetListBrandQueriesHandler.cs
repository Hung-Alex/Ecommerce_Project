using Application.Common.Interface;
using AutoMapper;
using Domain.Shared;
using MediatR;
using Application.DTOs.Responses.Brands;
using Domain.Entities.Brands;
using Application.Features.Brands.Specification;


namespace Application.Features.Brands.Queries.Get
{
    public sealed class GetListBrandQueriesHandler : IRequestHandler<GetListBrandsQuery, Result<IEnumerable<BrandDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetListBrandQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<BrandDTO>>> Handle(GetListBrandsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var getBrandSpecification = new GetBrandsSpecification(request.BrandFilter);
            var brands = await repo.GetAllAsync(getBrandSpecification);
            var totalItems = await repo.CountAsync(getBrandSpecification);
            return new PagingResult<IEnumerable<BrandDTO>>(_mapper.Map<IEnumerable<BrandDTO>>(brands), request.BrandFilter.PageNumber, request.BrandFilter.PageSize, totalItems);
        }
    }
}

