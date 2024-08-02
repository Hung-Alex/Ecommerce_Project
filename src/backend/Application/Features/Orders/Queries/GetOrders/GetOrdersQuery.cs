
using Application.DTOs.Responses.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders
{
    public record GetOrdersQuery:IRequest<Result<List<OrderDTO>>>
    {
    }
}
