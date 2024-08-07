using Application.DTOs.Filters.Slides;
using Application.Features.Slides.Commands.CreateSlide;
using Application.Features.Slides.Commands.DeleteSlide;
using Application.Features.Slides.Commands.UpdateSlide;
using Application.Features.Slides.Queries.Get;
using Application.Features.Slides.Queries.GetById;
using Application.Features.Slides.Queries.GetSlideActive;
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
    [Route("api/slides")]
    public class SlideController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SlideController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadSlide, Permission.UpdateSlide])]
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
        [HasPermission(Permission.ReadSlide)]
        public async Task<ActionResult> GetSlides([FromQuery] SlideFilter slideFilter)
        {
            var result = await _mediator.Send(new GetListSlideQuery(slideFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        [HasPermission(Permission.UpdateSlide)]
        public async Task<ActionResult> UpadateSlide(Guid id, [FromForm] UpdateSlideCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdateSlideCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteSlide)]
        public async Task<ActionResult> DeleteSlide(Guid id)
        {
            var result = await _mediator.Send(new DeleteSlideCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpPost]
        [FileValidatorFilter<CreateSlideCommand>([".png", ".jpg"], 1920 * 1080)]
        [HasPermission(Permission.CreateSlide)]
        public async Task<IActionResult> AddSlide([FromForm] CreateSlideCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
