using Application.DTOs.Request;
using Application.Features.Carts.Commands.AddItem;
using Application.Features.Carts.Commands.DeleteItem;
using Application.Features.Carts.Commands.UpdateQuanity;
using Application.Features.Carts.Queries.GetItemInCart;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult> GetItemsInCart()
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new GetItemsInCartQuery(Guid.Parse(claimUser.Value)));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddItemInCart([FromBody] CartItemRequest cartItem)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new AddItemCommand(Guid.Parse(claimUser.Value), cartItem.ProductId, cartItem.Quantity));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteItemInCart(Guid id)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new DeleteItemCommand(Guid.Parse(claimUser.Value), id));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateQuantiy([FromBody] UpdateQuantityItemRequest updateQuantity)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new UpdateQuantityItemCommand(Guid.Parse(claimUser.Value), updateQuantity.CartItemId, updateQuantity.quantity));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
    }
}
