using Application.Features.Sections.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/sections")]
    public class SectionController:ControllerBase
    {
        private readonly IMediator _mediator;
        public SectionController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a list of sections
        /// </summary>
        /// <param name="query">The query parameters for filtering sections</param>
        /// <returns>A list of sections if successful, otherwise a 400 result</returns>
        [HttpGet]
        public async Task<ActionResult> GetSections([FromQuery] GetSectionsQuery query)
        {
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
