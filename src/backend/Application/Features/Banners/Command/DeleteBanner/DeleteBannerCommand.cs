using Domain.Shared;
using MediatR;


namespace Application.Features.Banners.Commands.DeleteBanner
{ 
    public record DeleteBannerCommand(Guid Id) : IRequest<Result<bool>>;
}
