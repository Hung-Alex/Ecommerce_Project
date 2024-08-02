
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders
{
    public record GetOrdersQuery:IRequest<Result<List<OrderDto>>>
    {
    }
}
