using Application.DTOs.Request;
using Application.DTOs.Responses.Cart;
using Application.Features.Carts.Commands.AddItem;
using Application.Features.Carts.Commands.DeleteItem;
using Application.Features.Carts.Commands.UpdateQuanity;
using Application.Features.Carts.Queries.GetItemInCart;
using Domain.Constants;
using Domain.Shared;
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
        /// <summary>
        /// Retrieves all items in the user's cart.
        /// </summary>
        /// <returns>A list of items currently in the cart.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Result<CartDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<CartDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetItemsInCart()
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new GetItemsInCartQuery(Guid.Parse(claimUser.Value)));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Adds an item to the user's cart.
        /// </summary>
        /// <param name="cartItem">The item to add to the cart, including the product ID and quantity.</param>
        /// <returns>Result of the add item operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddItemInCart([FromBody] CartItemRequest cartItem)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new AddItemCommand(Guid.Parse(claimUser.Value), cartItem.ProductId, cartItem.Quantity));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Deletes an item from the user's cart.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>Result of the delete item operation.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteItemInCart(Guid id)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new DeleteItemCommand(Guid.Parse(claimUser.Value), id));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Updates the quantity of an item in the user's cart.
        /// </summary>
        /// <param name="updateQuantity">The request containing the item ID and the new quantity.</param>
        /// <returns>Result of the update quantity operation.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateQuantity([FromBody] UpdateQuantityItemRequest updateQuantity)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new UpdateQuantityItemCommand(Guid.Parse(claimUser.Value), updateQuantity.CartItemId, updateQuantity.quantity));
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
    }
}
