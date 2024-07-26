using Application.DTOs.Filters.Slides;
using Application.Features.Slides.Commands.CreateSlide;
using Application.Features.Slides.Commands.DeleteSlide;
using Application.Features.Slides.Commands.UpdateSlide;
using Application.Features.Slides.Queries.Get;
using Application.Features.Slides.Queries.GetById;
using Application.Features.Slides.Queries.GetSlideActive;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/slides")]
    public class SlideController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SlideController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetSlideByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("is-actice")]
        public async Task<ActionResult> GetSlideIsActive()
        {
            var result = await _mediator.Send(new GetSlideIsActiveQuery());
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetSlides([FromQuery] SlideFilter slideFilter)
        {
            var result = await _mediator.Send(new GetListSlideQuery(slideFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpadateSlide(Guid id, [FromForm] UpdateSlideCommand command)
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
        public async Task<ActionResult> DeleteSlide(Guid id)
        {
            var result = await _mediator.Send(new DeleteSlideCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddSlide([FromForm] CreateSlideCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
