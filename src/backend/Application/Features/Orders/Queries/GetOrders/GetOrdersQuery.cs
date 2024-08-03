using Application.DTOs.Filters.Orders;
using Application.DTOs.Responses.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(OrderFilter _filter) : IRequest<Result<IEnumerable<OrderDTO>>>;
}
