using Application.Features.Roles.Command.CreateRole;
using Application.Features.Roles.Command.DeleteRole;
using Application.Features.Roles.Command.UpdateRole;
using Application.Features.Roles.Queries.GetRoleById;
using Application.Features.Roles.Queries.GetRoles;
using Domain.Constants;
using Domain.Shared;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Domain.Enums.PermissionEnum;

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
        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="command">The create command</param>
        /// <returns>The created role if successful, otherwise a 400 result</returns>
        [HttpPost]
        [HasPermission(Permission.CreateRole)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="id">The ID of the role to delete</param>
        /// <returns>A 200 result if successful, otherwise a 400 result</returns>
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteRole)]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="id">The ID of the role to update</param>
        /// <param name="command">The update command</param>
        /// <returns>The updated role if successful, otherwise a 400 result</returns>
        [HttpPut("{id:Guid}")]
        [HasPermission(Permission.UpdateRole)]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleCommand command)
        {
            if (id != command.RoleId)
            {
                return BadRequest(Result<UpdateRoleCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of roles
        /// </summary>
        /// <returns>A list of roles</returns>
        [HttpGet]
        [HasPermission(Permission.ReadRole)]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets a role by its ID
        /// </summary>
        /// <param name="id">The ID of the role</param>
        /// <returns>The role if found, otherwise a 404 result</returns>
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadRole, Permission.UpdateRole])]
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
