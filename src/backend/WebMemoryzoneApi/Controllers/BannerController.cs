using Application.DTOs.Filters.Banner;
using Application.DTOs.Responses.Banners;
using Application.Features.Banners.Commands.CreateBanner;
using Application.Features.Banners.Commands.DeleteBanner;
using Application.Features.Banners.Commands.UpdateBanner;
using Application.Features.Banners.Queries.Get;
using Application.Features.Banners.Queries.GetBannerIsVisiable;
using Application.Features.Banners.Queries.GetById;
using Domain.Constants;
using Domain.Shared;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;
using static Domain.Enums.PermissionEnum;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/banners")]
    public class BannerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BannerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a banner by its ID.
        /// </summary>
        /// <param name="id">The ID of the banner.</param>
        /// <returns>The banner details or an error if not found.</returns>
        /// <response code="200">Returns the banner details with IsSuccess set to true.</response>
        /// <response code="404">Returns an error with IsSuccess set to false and data set to null.</response>
        /// <response code="500">Returns an error </response>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(Result<BannerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<BannerDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission(PermissionOperator.Or, [Permission.UpdateBanner, Permission.ReadBanner])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBannerByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of banners.
        /// </summary>
        /// <param name="bannerFilter">The filter criteria for retrieving banners.</param>
        /// <returns>
        /// A <see cref="ActionResult"/> containing the list of banners or an error message if the request fails.
        /// </returns>
        /// <response code="200">Returns a list of banners with IsSuccess set to true.</response>
        /// <response code="500">Returns an error.</response>
        [HttpGet]
        [HasPermission(Permission.ReadBanner)]
        [ProducesResponseType(typeof(PagingResult<BannerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBanners([FromQuery] BannerFilter bannerFilter)
        {
            var result = await _mediator.Send(new GetListBannerQuery(bannerFilter));
            return Ok(result);
        }
        /// <summary>
        /// get banners is visible.
        /// </summary>
        /// <returns>
        /// A <see cref="IActionResult"/> indicating whether the banner is visible (200 OK) or not (404 Not Found).
        /// </returns>
        /// <response code="200">Returns banners and IsSuccess set to true.</response>
        /// <response code="404">Returns an error with IsSuccess set to false and data set to null.</response>
        /// <response code="500">Returns an error </response>
        [AllowAnonymous]
        [HttpGet("is-visable")]
        [ProducesResponseType(typeof(Result<IEnumerable<BannerDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBannerIsVisiable()
        {
            var result = await _mediator.Send(new GetBannerIsVisiableQuery());
            return Ok(result);
        }
        /// <summary>
        /// Updates a banner.
        /// </summary>
        /// <param name="id">The ID of the banner.</param>
        /// <param name="command">The update banner command.</param>
        /// <returns>
        /// A <see cref="ActionResult"/> containing the updated banner details if successful, otherwise a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns IsSuccess set to true.</response>
        /// <response code="400">Returns an error with IsSuccess set to false and data set to null.</response>
        /// <response code="500">Returns an error </response>
        [HttpPut("{id:Guid}")]
        [FileValidatorFilter<UpdateBannerCommand>([".png", ".jpg"], 1024 * 1024)]
        [HasPermission(Permission.UpdateBanner)]
        [ProducesResponseType(typeof(Result<BannerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<BannerDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpadateBanner(Guid id, [FromForm] UpdateBannerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdateBannerCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Deletes a banner
        /// </summary>
        /// <param name="id">The banner ID</param>
        /// <returns>
        /// A <see cref="ActionResult"/> indicating whether the banner was deleted successfully (200 OK) or not (404 Not Found).
        /// </returns>
        /// <response code="200">Returns IsSuccess set to true.</response>
        /// <response code="404">Returns an error with IsSuccess set to false and data set to null.</response>
        /// <response code="500">Returns an error </response>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        [HasPermission(Permission.DeleteBanner)]
        public async Task<ActionResult> DeleteBanner(Guid id)
        {
            var result = await _mediator.Send(new DeleteBannerCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Creates a new banner
        /// </summary>
        /// <param name="command">The create banner command</param>
        /// <returns>
        /// A <see cref="IActionResult"/> containing the created banner details if successful, otherwise a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns IsSuccess set to true.</response>
        /// <response code="400">Returns an error with IsSuccess set to false and data set to null.</response>
        /// <response code="500">Returns an error </response>
        [HttpPost]
        [FileValidatorFilter<CreateBannerCommand>([".png", ".jpg"], 1024 * 1024)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [HasPermission(Permission.CreateBanner)]
        public async Task<IActionResult> AddBanner([FromForm] CreateBannerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
