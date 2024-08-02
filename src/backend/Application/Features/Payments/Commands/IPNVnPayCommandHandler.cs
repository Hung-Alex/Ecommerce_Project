using Application.Common.Interface;
using Application.Features.State.Specification;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Shared;
using MediatR;

namespace Application.Features.Payments.Commands
{
    public sealed class IPNVnPayCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<IPNVnPayCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(IPNVnPayCommand request, CancellationToken cancellationToken)
        {
            var repoOrder = unitOfWork.GetRepository<Order>();
            var repoPayment = unitOfWork.GetRepository<Payment>();
            var repoStatus =  unitOfWork.GetRepository<Status>();
            var statusCompleted = await repoStatus.FindOneAsync(new GetStatusByCodeSpecification("Completed"));
            var statusFail = await repoStatus.FindOneAsync(new GetStatusByCodeSpecification("Failed"));        
            var order= await repoOrder.GetByIdAsync(request.OrderId);
            if (order is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.OrderId));
            }
            var payment=await repoPayment.GetByIdAsync(order.PaymentId);
            if (request.Code=="00")
            {
                payment.TransactionId = request.TransactionId;
                payment.TransactionDate = DateTimeOffset.Now;
                payment.StatusId = statusCompleted.Id;
                order.StatusId = statusCompleted.Id;
            }
            else
            {
                payment.TransactionId = request.TransactionId;
                payment.TransactionDate = DateTimeOffset.Now;
                payment.StatusId = statusFail.Id;
                order.StatusId = statusFail.Id;
            }
            repoPayment.Update(payment);
            repoOrder.Update(order);
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
