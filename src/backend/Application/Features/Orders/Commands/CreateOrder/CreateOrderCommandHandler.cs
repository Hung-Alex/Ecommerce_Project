using Application.Common.Interface;
using Application.Common.Interface.Payments;
using Application.Common.Interface.RepositoryExtension;
using Application.DTOs.Responses.Payments;
using Application.Features.Carts.Specification;
using Application.Features.State.Specification;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Enums;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler(IUnitOfWork unitOfWork, ICartRepositoryExtension cartRepository,IVnPayService vnPayService) : IRequestHandler<CreateOrderCommand, Result<PaymentsResultDTO>>
    {
        public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
        {
            public CreateOrderCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().MustAsync(ValidationExtension.ValidateEmail);
                RuleFor(x => x.Phone).NotEmpty().Must(ValidationExtension.ValidatePhone);
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.PaymentMethod).IsInEnum();
            }
        }
        public async Task<Result<PaymentsResultDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var repoOrder = unitOfWork.GetRepository<Order>();
            var repoPayment = unitOfWork.GetRepository<Payment>();
            var repoStatus = unitOfWork.GetRepository<Status>();
            var statusPending = await repoStatus.FindOneAsync(new GetStateByCodeSpecification("PENDING"));
            if (statusPending == null)
            {
                return Result<PaymentsResultDTO>.ResultFailures(ErrorConstants.ProcessError.StatusPendingNotFound);
            }
            var cart = await cartRepository.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            if (cart == null)
            {
                return Result<PaymentsResultDTO>.ResultFailures(ErrorConstants.CartError.CartNotFound);
            }
            var getCartItems = await cartRepository.GetCartAsync(cart.Id);
            var totalAmount = getCartItems.Total;
            var hasOutOfStockItems = getCartItems.Items.Any(x => x.IsStock == false);
            if (hasOutOfStockItems)
            {
                return Result<PaymentsResultDTO>.ResultFailures(ErrorConstants.CartError.ProductOutOfStock);
            }
            // Create and add payment
            var payment = new Payment(totalAmount, request.PaymentMethod, DateTime.Now, 5000m, statusPending.Id);
            // Create and add order
            var order = new Order(
                new ShipAddress(request.Name, request.Email, request.Phone, request.Address),
                request.Note,
                request.UserId,
                payment.Id,
                statusPending.Id
            );
            var orderItems = getCartItems.Items.Select(x => new OrderItems(order.Id, x.ProductId, x.Quantity, x.Price, "VND")).ToList();
            order.AddOrderItems(orderItems);
            payment.Orders.Add(order);
            repoPayment.Add(payment);
            // Clear cart items
            cart.CartItems.Clear();  // Update cart items collection
            cartRepository.Update(cart);  // Persist changes to database
            // Commit transaction
            await unitOfWork.Commit();
            if (request.PaymentMethod==PaymentMethod.VnPay)
            {
                var url= await vnPayService.CreatePaymentUrl(order, payment, totalAmount, cancellationToken);
                return Result<PaymentsResultDTO>.ResultSuccess(new PaymentsResultDTO
                {
                    PaymentUrl = url.Data.PaymentUrl
                });
            }
            else
            {
                return Result<PaymentsResultDTO>.ResultSuccess(new PaymentsResultDTO
                {
                    PaymentUrl = null
                });
            }
        }
    }
}
