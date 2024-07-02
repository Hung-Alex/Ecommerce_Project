using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Banners.Commands.CreateBanner

{
    public record CreateBannerCommand(string Title, string Description,bool ? left, bool? right, IFormFile FormFile) : IRequest<Result<bool>>, IValidatableRequest;
}
