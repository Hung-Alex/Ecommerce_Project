using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.DTOs.Responses.Banners;
using Domain.Entities.Banners;
using Application.Features.Banners.Specification;

namespace Application.Features.Banners.Queries.GetById
{
    public class GetBannerByIdQueryHandler : IRequestHandler<GetBannerByIdQuery, Result<BannerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetBannerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<BannerDTO>> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Banner>();
            var getBannerByIdSpecification = new GetBannerByIdSepecification(request.Id);
            var banner = await repo.FindOneAsync(getBannerByIdSpecification);
            if (banner == null) return Result<BannerDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id)); ;
            var bannerDTO = _mapper.Map<BannerDTO>(banner);
            return Result<BannerDTO>.ResultSuccess(bannerDTO);
        }
    }
}
