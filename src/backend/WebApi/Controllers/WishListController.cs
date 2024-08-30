using Application.DTOs.Filters.WishList;
using Application.Features.WishsList.Commands.CreateFavoriteProduct;
using Application.Features.WishsList.Commands.DeleteFavoriteProduct;
using Application.Features.WishsList.Queries.GetListFavoriteProducts;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/wishlist")]
    public class WishListController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WishListController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Deletes a favorite product from the wish list
        /// </summary>
        /// <param name="productId">The ID of the product to delete</param>
        /// <returns>A 200 result if successful, otherwise a 404 result</returns>
        [HttpDelete("{productId:Guid}")]
        public async Task<ActionResult> DeleteFavouriteProduct(Guid productId)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new DeleteFavoriteProductCommand(productId, Guid.Parse(claimUser.Value)));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
        /// <summary>
        /// Adds a product to the wish list
        /// </summary>
        /// <param name="productId">The ID of the product to add</param>
        /// <returns>A 200 result if successful, otherwise a 400 result</returns>
        [HttpPost]
        public async Task<IActionResult> AddFavouriteProduct(Guid productId)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new AddFavoriteProductCommand(Guid.Parse(claimUser.Value), productId));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Retrieves the wish list for the current user
        /// </summary>
        /// <param name="filter">The filter parameters for the wish list</param>
        /// <returns>The wish list for the current user</returns>
        [HttpGet]
        public async Task<ActionResult> GetWishList([FromQuery] WishListFilter filter)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var result = await _mediator.Send(new GetListFavoriteProductsQuery(filter, Guid.Parse(claimUser.Value)));
            return Ok(result);
        }
    }
}
