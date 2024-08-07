using Application.Features.Images.Command;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Domain.Enums.PermissionEnum;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteImage)]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var result = await _mediator.Send(new DeleteImageCommand(id));
            if (result.IsSuccess is false)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
