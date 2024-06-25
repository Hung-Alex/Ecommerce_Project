using Application.DTOs.Filters.Tags;
using Application.Features.Rattings.Commands.CreateRatting;
using Application.Features.Rattings.Commands.DeleteRatting;
using Application.Features.Rattings.Commands.UpdateRatting;
using Application.Features.Rattings.Queries.GetById;
using Application.Features.Tags.Commands.CreateTag;
using Application.Features.Tags.Commands.DeleteTag;
using Application.Features.Tags.Commands.UpdateTag;
using Application.Features.Tags.Queries.Get;
using Application.Features.Tags.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/rattings")]
    public class RattingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RattingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetRattingByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
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
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteRatting(Guid id)
        {
            var result = await _mediator.Send(new DeleteRattingCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddRatting([FromBody] CreateRattingCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
    }
}
