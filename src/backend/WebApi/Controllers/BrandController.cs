using Application.DTOs.Filters.Brands;
using Application.DTOs.Responses.Auth;
using Application.DTOs.Responses.Brands;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.Get;
using Application.Features.Brands.Queries.GetById;
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
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a brand by its ID.
        /// </summary>
        /// <param name="id">The ID of the brand to retrieve.</param>
        /// <returns>
        /// A <see cref="Task{ActionResult}"/> containing the brand details if found, otherwise a 404 Not Found response.
        /// </returns>
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadBrand, Permission.UpdateBrand])]
        [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBrandByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Retrieves a list of brands based on filters.
        /// </summary>
        /// <param name="brandFilter">The filter criteria for retrieving brands.</param>
        /// <returns>
        /// A <see cref="Task{ActionResult}"/> containing a list of brands.
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(PagingResult<BrandDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBrands([FromQuery] BrandFilter brandFilter)
        {
            var result = await _mediator.Send(new GetListBrandsQuery(brandFilter));
            return Ok(result);
        }
        /// <summary>
        /// Updates an existing brand.
        /// </summary>
        /// <param name="id">The ID of the brand to update.</param>
        /// <param name="command">The command containing updated brand information.</param>
        /// <returns>
        /// A <see cref="Task{ActionResult}"/> indicating the result of the update operation.
        /// </returns>
        [HttpPut("{id:Guid}")]
        [FileValidatorFilter<UpdateBrandCommand>([".png", ".jpg"], 1024 * 1024)]
        [HasPermission(Permission.UpdateBrand)]
        [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpadateBrand(Guid id, [FromForm] UpdateBrandCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdateBrandCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Deletes a brand by its ID.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <returns>
        /// A <see cref="Task{ActionResult}"/> indicating whether the brand was successfully deleted.
        /// </returns>
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteBrand)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Adds a new brand.
        /// </summary>
        /// <param name="command">The command containing brand information to add.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> indicating the result of the add operation.
        /// </returns>
        [HttpPost]
        [FileValidatorFilter<CreateBrandCommand>([".png", ".jpg"], 1024 * 1024)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        [HasPermission(Permission.CreateBrand)]
        public async Task<IActionResult> AddBrand([FromForm] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
