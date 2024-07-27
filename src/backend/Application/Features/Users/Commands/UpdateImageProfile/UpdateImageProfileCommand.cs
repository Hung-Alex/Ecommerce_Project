using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Application.Features.Users.Commands.UpdateImageProfile
{
    public record UpdateImageProfileCommand(Guid UserId, IFormFile Image) : IRequest<Result<bool>>;
}
