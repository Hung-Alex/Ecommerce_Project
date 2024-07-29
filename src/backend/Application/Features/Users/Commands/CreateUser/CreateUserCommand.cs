using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Result<bool>>
    {
        public string UserName { get; init; }//account login
        public string Email { get; init; }//is unique
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Region { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }
        public bool IsLocked { get; init; }
        public IEnumerable<Guid> Roles { get; init; }
    }
}
