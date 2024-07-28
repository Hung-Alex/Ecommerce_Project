using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<Result<bool>>
    {
        public Guid UserId { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Region { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }
        public bool IsActive { get; init; }
        public IEnumerable<Guid>? Roles { get; init; }
    }
}
