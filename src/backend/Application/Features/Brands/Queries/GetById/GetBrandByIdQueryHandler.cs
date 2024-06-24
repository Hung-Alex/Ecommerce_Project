using Application.Common.Interface;
using Application.Features.Brands.Specification;
using Application.DTOs.Responses.Brand;
using AutoMapper;
using Domain.Entities.Brands;
using MediatR;
using Domain.Shared;
using Domain.Constants;

namespace Application.Features.Brands.Queries.GetById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<BrandDTOs>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<BrandDTOs>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var getProductByIdSpecification = new GetBrandByIdSepecification(request.Id);
            var brand = await repo.FindOneAsync(getProductByIdSpecification);
            if (brand == null) return Result<BrandDTOs>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id)); ;
            var brandDTO = _mapper.Map<BrandDTOs>(brand);
            return Result<BrandDTOs>.ResultSuccess(brandDTO);
        }
    }
}
