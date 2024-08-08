using Application.DTOs.Filters.Comments;
using Application.Features.Comments.Commands;
using Application.Features.Comments.Queries.GetCommentByPostId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Adds a new comment
        /// </summary>
        /// <param name="command">The add comment command</param>
        /// <returns>The result of adding the comment</returns>
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets comments by post ID
        /// </summary>
        /// <param name="filter">The comment filter</param>
        /// <returns>A list of comments for the specified post ID</returns>
        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId([FromQuery] CommentFilter filter)
        {
            var result = await _mediator.Send(new GetCommentByPostIdQuery(filter));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
