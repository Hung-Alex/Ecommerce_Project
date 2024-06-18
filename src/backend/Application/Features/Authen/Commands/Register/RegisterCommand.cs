using Domain.Behavior;
using MediatR;

namespace Application.Features.Authen.Commands.Register
{
    public record RegisterCommand(string userName, string Email, string Password, string ConfirmPassword) : IRequest, IValidatableRequest;
}
