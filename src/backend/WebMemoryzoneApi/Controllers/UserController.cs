using Application.DTOs.Filters.Users;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Users.Commands.ChangePassword;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.LockAccount;
using Application.Features.Users.Commands.UpdateImageProfile;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Queries.GetUserById;
using Application.Features.Users.Queries.GetUsers;
using Domain.Constants;
using Domain.Shared;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static Domain.Enums.PermissionEnum;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a user by ID
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <returns>The user if found, otherwise a 404 result</returns>
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadUser, Permission.UpdateUser])]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            if (result.IsSuccess is false)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Retrieves a list of users
        /// </summary>
        /// <param name="filter">The filter parameters for the users</param>
        /// <returns>A list of users</returns>
        [HttpGet]
        [HasPermission(Permission.ReadUser)]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilter filter)
        {
            var result = await _mediator.Send(new GetUsersQuery(filter));
            return Ok(result);
        }
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="command">The create user command</param>
        /// <returns>The created user if successful, otherwise a 400 result</returns>
        [HttpPost]
        [HttpPost]
        [HasPermission(Permission.CreateUser)]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Updates a user's avatar image
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <param name="command">The update image profile command</param>
        /// <returns>The updated user if successful, otherwise a 400 result</returns>
        [HttpPut("avatar/{id:guid}")]
        public async Task<IActionResult> UpdateAvatarImage(Guid id, [FromForm] UpdateImageProfileCommand command)
        {
            if (id != command.UserId)
            {
                return BadRequest(Result<UpdateImageProfileCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Locks a user's account
        /// </summary>
        /// <param name="command">The lock account command</param>
        /// <returns>A 200 result if successful, otherwise a 400 result</returns>
        [HttpPost("lock-account")]
        public async Task<IActionResult> LockAccount([FromBody] LockAccountCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <param name="command">The update user command</param>
        /// <returns>The updated user if successful, otherwise a 400 result</returns>
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromForm] UpdateUserCommand command)
        {
            if (id != command.UserId)
            {
                return BadRequest(Result<UpdateUserCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Changes a user's password
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <param name="command">The change password command</param>
        /// <returns>A 200 result if successful, otherwise a 400 result</returns>
        [HttpPut("{id:guid}/change-password")]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordCommand command)
        {
            if (id != command.UserId)
            {
                return BadRequest(Result<ChangePasswordCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
