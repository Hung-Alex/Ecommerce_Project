using Application.Common.Interface;
using Application.DTOs.Responses.Orders;
using Application.Features.Orders.Specification;
using Domain.Entities.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders
{
    public sealed class GetOrdersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrdersQuery, Result<IEnumerable<OrderDTO>>>
    {
        public async Task<Result<IEnumerable<OrderDTO>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Order>();
            var spec = new GetOrdersSpecification(request._filter);
            var orders = await repo.GetAllAsync(spec);
            var data = orders.Select(x => new OrderDTO
            {
                Id = x.Id,
                ShipAddress = x.ShipAddress,
                Status = x.Status.Display,
                OrderItems = x.OrderItems.Select(x => new OrderItemsDTO
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    UnitPrice = x.UnitPrice
                }).ToList()
            });
            var totalItems = await repo.CountAsync(spec);
            return new PagingResult<IEnumerable<OrderDTO>>(data, request._filter.PageNumber, request._filter.PageSize, totalItems);
        }
    }
}
