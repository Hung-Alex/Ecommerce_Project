using Application.Common.Interface;
using Application.Features.Authen.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("authencations")]
    public class AuthencationController:ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthencationController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async  Task<ActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
