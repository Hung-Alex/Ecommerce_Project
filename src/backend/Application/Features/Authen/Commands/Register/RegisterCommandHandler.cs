using Application.Common.Interface.IdentityService;
using MediatR;

namespace Application.Features.Authen.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IIdentityService _identityService;
        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {    
            await _identityService.CreateUserAsync(request.Email, request.Password, request.userName);
        }
    }
}
