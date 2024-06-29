using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Images.Commands.CreateImage;
using Application.Features.Images.Commands.DeleteImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController:ControllerBase
    {
        private readonly IMediator _mediator;
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] CreateImageCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var result = await _mediator.Send(new DeleteImageCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
    }
}
