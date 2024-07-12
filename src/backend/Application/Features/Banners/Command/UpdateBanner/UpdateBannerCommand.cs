using Application.DTOs.Responses.Banners;
using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Domain.Enums.BannerEnum;

namespace Application.Features.Banners.Commands.UpdateBanner
{
    public sealed record UpdateBannerCommand(Guid Id, string Title, string Description, LocationBanner? Location, IFormFile? FormFile) : IRequest<Result<BannerDTO>>, IValidatableRequest;
}
