using Application.DTOs.Filters.Categories;
using Application.DTOs.Filters.Tags;
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
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTagByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetTags([FromQuery] TagFilter tagFilter)
        {
            var result = await _mediator.Send(new GetListTagsQuery(tagFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpadateTag(Guid id, [FromBody] UpdateTagCommand command)
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
        public async Task<ActionResult> DeleteTag(Guid id)
        {
            var result = await _mediator.Send(new DeleteTagCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetTagByUrlSlug(string slug)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateTagCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
    }
}
