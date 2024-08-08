using Application.DTOs.Filters.State;
using Application.Features.State.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a list of states
        /// </summary>
        /// <param name="filter">The filter parameters for the states</param>
        /// <returns>A list of states</returns>
        [HttpGet]
        public async Task<IActionResult> GetStates([FromQuery] StateFilter filter)
        {
            var result = await _mediator.Send(new GetStateQuery(filter));
            return Ok(result);
        }
    }
}
