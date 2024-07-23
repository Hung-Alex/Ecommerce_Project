using Application.DTOs.Responses.Banners;
using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Banners.Commands.UpdateBanner
{
    public sealed record UpdateBannerCommand(Guid Id, string Title, string Description, IFormFile? FormFile,bool Visible) : IRequest<Result<BannerDTO>>, IValidatableRequest;
}
