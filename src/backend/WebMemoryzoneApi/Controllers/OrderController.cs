using Application.DTOs.Filters.Orders;
using Application.DTOs.Request;
using Application.Features.Orders.Commands.CancelOrder;
using Application.Features.Orders.Commands.ChangeStatusOrder;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetOrders;
using Application.Features.Orders.Queries.GetOrderUser;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilter orderFilter)
        {
            var result = await _mediator.Send(new GetOrdersQuery(orderFilter));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
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
        [HttpPost("change-status-order")]
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
