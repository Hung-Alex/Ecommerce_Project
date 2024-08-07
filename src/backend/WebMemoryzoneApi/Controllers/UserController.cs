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
        [HttpGet]
        [HasPermission(Permission.ReadUser)]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilter filter)
        {
            var result = await _mediator.Send(new GetUsersQuery(filter));
            return Ok(result);
        }
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
