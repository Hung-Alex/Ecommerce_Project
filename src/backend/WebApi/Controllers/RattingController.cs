using Application.DTOs.Filters.Rattings;
using Application.Features.Rattings.Commands.CreateRatting;
using Application.Features.Rattings.Commands.DeleteRatting;
using Application.Features.Rattings.Commands.UpdateRatting;
using Application.Features.Rattings.Queries.GetById;
using Application.Features.Rattings.Queries.GetRattingProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/rattings")]
    public class RattingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RattingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a ratting by its ID
        /// </summary>
        /// <param name="id">The ID of the ratting</param>
        /// <returns>The ratting if found, otherwise a 404 result</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetRattingByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of rattings
        /// </summary>
        /// <param name="filter">The filter to apply to the rattings</param>
        /// <returns>A list of rattings</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetRattings([FromQuery] RattingFilter filter)
        {
            var result = await _mediator.Send(new GetRattingProductByIdQuery(filter));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Updates a ratting
        /// </summary>
        /// <param name="id">The ID of the ratting to update</param>
        /// <param name="command">The update command</param>
        /// <returns>The updated ratting if successful, otherwise a 400 result</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpadateRatting(Guid id, [FromBody] UpdateRattingCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Deletes a ratting
        /// </summary>
        /// <param name="id">The ID of the ratting to delete</param>
        /// <returns>A 200 result if successful, otherwise a 404 result</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteRatting(Guid id)
        {
            var result = await _mediator.Send(new DeleteRattingCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Creates a new ratting
        /// </summary>
        /// <param name="command">The create command</param>
        /// <returns>The created ratting if successful, otherwise a 400 result</returns>
        [HttpPost]
        public async Task<IActionResult> AddRatting([FromBody] CreateRattingCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
