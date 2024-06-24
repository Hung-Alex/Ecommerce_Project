using Application.Common.Interface.IdentityService;
using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Guid>>
    {
        private readonly IIdentityService _identityService;
        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.CreateUserAsync(request.Email, request.Password, request.userName);
            return Result<Guid>.ResultSuccess(userId);
        }
    }
}
