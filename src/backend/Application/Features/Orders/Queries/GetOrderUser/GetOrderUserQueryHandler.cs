using Application.Common.Interface;
using Application.DTOs.Responses.Orders;
using Application.Features.Orders.Specification;
using Domain.Entities.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderUser
{
    public sealed class GetOrderUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderUserQuery, Result<IEnumerable<OrderDTO>>>
    {
        public async Task<Result<IEnumerable<OrderDTO>>> Handle(GetOrderUserQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Order>();
            var spec = new GetUserOrderSpecification(request.filter, request.UserId);
            var orders = await repo.GetAllAsync(spec);
            var data = orders.Select(x => new OrderDTO
            {
                Id = x.Id,
                ShipAddress = x.ShipAddress,
                Status = x.Status.Display,
                CancelReason = x.CancelReason,
                PaymentStatus = x.Payment.Status.Display,
                PaymentMethod = x.Payment.PaymentMethod.ToString(),
                TotalAmount = x.OrderItems.Sum(item => item.Quantity * item.Price),
                OrderItems = x.OrderItems.Select(x => new OrderItemsDTO
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    UnitPrice = x.UnitPrice
                }).ToList()
            });
            var totalItems = await repo.CountAsync(spec);
            return new PagingResult<IEnumerable<OrderDTO>>(data, request.filter.PageNumber, request.filter.PageSize, totalItems);
        }
    }
}