using Application.Features.Roles.Command.CreateRole;
using Application.Features.Roles.Command.DeleteRole;
using Application.Features.Roles.Command.UpdateRole;
using Application.Features.Roles.Queries.GetRoleById;
using Application.Features.Roles.Queries.GetRoles;
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
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(id));
            if (result.IsSuccess is false)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
