using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Images.Commands.CreateImage
{
    public record CreateImageCommand(IFormFile FormFile) : IRequest<Result<bool>>, IValidatableRequest;
}
