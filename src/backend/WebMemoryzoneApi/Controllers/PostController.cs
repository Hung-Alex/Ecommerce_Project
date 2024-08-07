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
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadPost, Permission.UpdatePost])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        [HasPermission(Permission.ReadPost)]
        public async Task<ActionResult> GetPosts([FromQuery] PostFilter postFilter)
        {
            var result = await _mediator.Send(new GetListPostQuery(postFilter));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("published")]
        public async Task<ActionResult> GetLatestPosts([FromQuery] PostFilter postFilter)
        {
            var result = await _mediator.Send(new GetPostPublishedQuery(postFilter));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
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
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeletePost)]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            var result = await _mediator.Send(new DeletePostCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetPostByUrlSlug(string slug)
        {
            var result = await _mediator.Send(new GetPostByUrlSlugQuery(slug));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
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
