using Application.DTOs.Filters.Tags;
using Application.DTOs.Filters.WishList;
using Application.Features.Tags.Queries.Get;
using Application.Features.WishsList.Commands.CreateFavoriteProduct;
using Application.Features.WishsList.Commands.DeleteFavoriteProduct;
using Application.Features.WishsList.Queries.GetListFavoriteProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpDelete("{productId:Guid}")]
        public async Task<ActionResult> DeleteFavouriteProduct(Guid productId)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new DeleteFavoriteProductCommand(productId, Guid.Parse(claimUser.Value)));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddFavouriteProduct(Guid productId)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new AddFavoriteProductCommand(Guid.Parse(claimUser.Value), productId));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> GetWishList([FromQuery] WishListFilter filter)
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetListFavoriteProductsQuery(filter, Guid.Parse(claimUser.Value)));
            return Ok(result);
        }
    }
}
