using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.DTOs.Responses.Brands;
using Domain.Entities.Brands;
using Application.Features.Brands.Specification;

namespace Application.Features.Brands.Queries.GetById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<BrandDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<BrandDTO>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var getBrandByIdSpecification = new GetBrandByIdSepecification(request.Id);
            var brand = await repo.FindOneAsync(getBrandByIdSpecification);
            if (brand == null) return Result<BrandDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var brandDTO = _mapper.Map<BrandDTO>(brand);
            return Result<BrandDTO>.ResultSuccess(brandDTO);
        }
    }
}
