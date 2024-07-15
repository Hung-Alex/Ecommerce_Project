using Application.DTOs.Responses.Banners;
using Domain.Shared;
using MediatR;

namespace Application.Features.Banners.Queries.GetBannerIsVisiable
{
    public record GetBannerIsVisiableQuery:IRequest<Result<IEnumerable<BannerDTO>>>;
}
