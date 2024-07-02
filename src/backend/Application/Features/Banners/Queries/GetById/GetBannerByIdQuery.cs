using Application.DTOs.Responses.Banners;
using Domain.Shared;
using MediatR;

namespace Application.Features.Banners.Queries.GetById
{
    public record GetBannerByIdQuery(Guid Id) : IRequest<Result<BannerDTO>>;
}
