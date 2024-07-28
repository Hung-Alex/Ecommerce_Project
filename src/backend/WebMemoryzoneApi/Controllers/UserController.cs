using Application.DTOs.Filters.Users;
using Application.Features.Users.Commands.ChangePassword;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.LockAccount;
using Application.Features.Users.Commands.UpdateImageProfile;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Queries.GetUserById;
using Application.Features.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        public async Task<IActionResult> GetUsers([FromQuery] UserFilter filter)
        {
            var result = await _mediator.Send(new GetUsersQuery(filter));
            return Ok(result);
        }
        [HttpPost]
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
                return BadRequest();
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
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.UserId)
            {
                return BadRequest();
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
                return BadRequest();
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
