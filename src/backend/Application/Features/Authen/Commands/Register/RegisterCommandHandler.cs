using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Entities.Carts;
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
                    var repo = _unitOfWork.GetRepository<Cart>();
                    var userId = await _identityService.CreateUserAsync(request.Email, request.Password, request.userName);
                    repo.Add(new Cart() { UserId = userId });
                    await _unitOfWork.Commit();
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
