using Application.DTOs.Responses.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderUser
{
    public record GetOrderUserQuery(Guid UserId) : IRequest<Result<IEnumerable<OrderDTO>>>;
}
