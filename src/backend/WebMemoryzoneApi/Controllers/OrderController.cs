using Application.DTOs.Request;
using Application.Features.Orders.Commands.CreateOrder;
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

    }
}
