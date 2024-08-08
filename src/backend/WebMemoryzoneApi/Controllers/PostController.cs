using Application.DTOs.Filters.Posts;
using Application.Features.Posts.Commands.CreatePost;
using Application.Features.Posts.Commands.DeletePost;
using Application.Features.Posts.Commands.UpdatePost;
using Application.Features.Posts.Queries.Get;
using Application.Features.Posts.Queries.GetById;
using Application.Features.Posts.Queries.GetByUrlSlug;
using Application.Features.Posts.Queries.GetPostPublished;
using Domain.Constants;
using Domain.Shared;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;
using static Domain.Enums.PermissionEnum;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a post by its ID
        /// </summary>
        /// <param name="id">The ID of the post</param>
        /// <returns>The post if found, otherwise a 404 result</returns>
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadPost, Permission.UpdatePost])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of posts
        /// </summary>
        /// <param name="postFilter">The filter to apply to the posts</param>
        /// <returns>A list of posts</returns>
        [HttpGet]
        [HasPermission(Permission.ReadPost)]
        public async Task<ActionResult> GetPosts([FromQuery] PostFilter postFilter)
        {
            var result = await _mediator.Send(new GetListPostQuery(postFilter));
            return Ok(result);
        }
        /// <summary>
        /// Gets the latest published posts
        /// </summary>
        /// <param name="postFilter">The filter to apply to the posts</param>
        /// <returns>A list of published posts</returns>
        [AllowAnonymous]
        [HttpGet("published")]
        public async Task<ActionResult> GetLatestPosts([FromQuery] PostFilter postFilter)
        {
            var result = await _mediator.Send(new GetPostPublishedQuery(postFilter));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="id">The ID of the post to update</param>
        /// <param name="command">The update command</param>
        /// <returns>The updated post if successful, otherwise a 400 result</returns>
        [HttpPut("{id:Guid}")]
        [HasPermission(Permission.UpdatePost)]
        public async Task<ActionResult> UpadatePost(Guid id, [FromForm] UpdatePostCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdatePostCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="id">The ID of the post to delete</param>
        /// <returns>A 200 result if successful, otherwise a 404 result</returns>
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeletePost)]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            var result = await _mediator.Send(new DeletePostCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Gets a post by its URL slug
        /// </summary>
        /// <param name="slug">The URL slug of the post</param>
        /// <returns>The post if found, otherwise a 404 result</returns>
        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetPostByUrlSlug(string slug)
        {
            var result = await _mediator.Send(new GetPostByUrlSlugQuery(slug));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="command">The create command</param>
        /// <returns>The created post if successful, otherwise a 400 result</returns>
        [HttpPost]
        [FileValidatorFilter<CreatePostCommand>([".png", ".jpg"], 1024 * 1024)]
        [HasPermission(Permission.CreatePost)]
        public async Task<IActionResult> AddPost([FromForm] CreatePostCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
