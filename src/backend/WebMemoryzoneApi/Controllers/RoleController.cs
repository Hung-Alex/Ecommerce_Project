using Application.Features.Roles.Command.CreateRole;
using Application.Features.Roles.Command.DeleteRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
