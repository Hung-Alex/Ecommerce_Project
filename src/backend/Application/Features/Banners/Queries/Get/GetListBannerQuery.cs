using Application.DTOs.Filters.Banner;
using Application.DTOs.Responses.Banners;
using Domain.Shared;
using MediatR;


namespace Application.Features.Banners.Queries.Get
{
    public record GetListBannerQuery(BannerFilter BannerFilter) : IRequest<Result<IEnumerable<BannerDTO>>>;

}
