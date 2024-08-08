using Application.DTOs.Filters.Orders;
using Application.DTOs.Request;
using Application.Features.Orders.Commands.CancelOrder;
using Application.Features.Orders.Commands.ChangeStatusOrder;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetOrders;
using Application.Features.Orders.Queries.GetOrderUser;
using Domain.Constants;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Enums.PermissionEnum;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="infoOrderRequest">The order information</param>
        /// <returns>The result of creating the order</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] InfoOrderRequest infoOrderRequest)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new CreateOrderCommand(infoOrderRequest.Name
                , infoOrderRequest.Email
                , infoOrderRequest.Phone
                , infoOrderRequest.Address
                , infoOrderRequest.Note
                , infoOrderRequest.PaymentMethod)
            { UserId = Guid.Parse(claimUser.Value) });
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="orderFilter">The order filter</param>
        /// <returns>A list of orders</returns>
        [HttpGet]
        [HasPermission(Permission.ReadOrder)]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilter orderFilter)
        {
            var result = await _mediator.Send(new GetOrdersQuery(orderFilter));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of orders for the current user
        /// </summary>
        /// <param name="orderFilter">The order filter</param>
        /// <returns>A list of orders for the current user</returns>
        [HttpGet("user")]
        public async Task<IActionResult> GetOrders([FromQuery] UserOrderFilter orderFilter)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new GetOrderUserQuery(Guid.Parse(claimUser.Value), orderFilter));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Cancels an order
        /// </summary>
        /// <param name="cancelOrderCommand">The cancel order command</param>
        /// <returns>The result of canceling the order</returns>
        [HttpPost("cancel-order")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderCommand cancelOrderCommand)
        {
            var result = await _mediator.Send(cancelOrderCommand);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Changes the status of an order
        /// </summary>
        /// <param name="command">The change status order command</param>
        /// <returns>The result of changing the order status</returns>
        [HttpPost("change-status-order")]
        [HasPermission(Permission.ChangeOrderStatus)]
        public async Task<IActionResult> ChangeStatusOrder([FromBody] ChangeStatusOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
