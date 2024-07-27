using Application.DTOs.Filters.Users;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.GetUserById;
using Application.Features.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
