using Application.Common.Interface;
using Application.DTOs.Responses.Banners;
using Application.Features.Banners.Specification;
using AutoMapper;
using Domain.Entities.Banners;
using Domain.Shared;
using MediatR;

namespace Application.Features.Banners.Queries.GetBannerIsVisiable
{
    public sealed class GetBannerIsVisiableQueryHandler(IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<GetBannerIsVisiableQuery, Result<IEnumerable<BannerDTO>>>
    {
        public async Task<Result<IEnumerable<BannerDTO>>> Handle(GetBannerIsVisiableQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Banner>();
            var query = new GetBannerIsVisiableSpecification();
            var result=await repo.GetAllAsync(query, cancellationToken);
            return Result<IEnumerable<BannerDTO>>.ResultSuccess(mapper.Map<IEnumerable<BannerDTO>>(result));
        }
    }
}
