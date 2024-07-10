using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Domain.Enums.BannerEnum;

namespace Application.Features.Banners.Commands.CreateBanner

{
    public record CreateBannerCommand(string Title, string Description, LocationBanner? Location, IFormFile FormFile) : IRequest<Result<bool>>, IValidatableRequest;
}
