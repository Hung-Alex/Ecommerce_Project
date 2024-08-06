using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Entities.Carts;
using Domain.Entities.Users;
using Domain.Shared;
using MediatR;
using System.Transactions;

namespace Application.Features.Authen.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Guid>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCommandHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var repoUser = _unitOfWork.GetRepository<User>();
                    var repoCart = _unitOfWork.GetRepository<Cart>();
                    var userDomain = new User() { FirstName = request.userName };
                    repoUser.Add(userDomain);
                    var userId = await _identityService.CreateUserAsync(request.Email, request.Password, request.userName, userDomain.Id);
                    repoCart.Add(new Cart() { UserId = userDomain.Id });
                    await _unitOfWork.CommitAsync();
                    transactionScope.Complete();
                    return Result<Guid>.ResultSuccess(userId);
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            };

        }
    }
}
