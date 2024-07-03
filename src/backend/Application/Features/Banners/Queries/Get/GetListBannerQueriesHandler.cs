using Application.Common.Interface;
using AutoMapper;
using Domain.Shared;
using MediatR;
using Domain.Entities.Banners;
using Application.DTOs.Responses.Banners;
using Application.Features.Banners.Specification;


namespace Application.Features.Banners.Queries.Get
{
    public class GetListBannerQueriesHandler : IRequestHandler<GetListBannerQuery, Result<IEnumerable<BannerDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetListBannerQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<BannerDTO>>> Handle(GetListBannerQuery request, CancellationToken cancellationToken)
        {
            var bannerRepo = _unitOfWork.GetRepository<Banner>();
            var getbannerSpecification = new GetBannersSpecification(request.BannerFilter);
            var banners = await bannerRepo.GetAllAsync(getbannerSpecification);
            var totalItems = await bannerRepo.CountAsync(getbannerSpecification);
            return new PagingResult<IEnumerable<BannerDTO>>(_mapper.Map<IEnumerable<BannerDTO>>(banners), request.BannerFilter.PageNumber, request.BannerFilter.PageSize, totalItems);
        }
    }
}
