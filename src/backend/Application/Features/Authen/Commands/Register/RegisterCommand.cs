using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.Register
{
    public record RegisterCommand(string userName, string Email, string Password, string ConfirmPassword) : IRequest<Result<Guid>>, IValidatableRequest;
}
