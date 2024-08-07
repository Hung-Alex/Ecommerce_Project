using Application.DTOs.Filters.Categories;
using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.Get;
using Application.Features.Category.Queries.GetById;
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
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadCategory, Permission.UpdateCategory])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetCategories([FromQuery] CategoryFilter categoryFilter)
        {
            var result = await _mediator.Send(new GetListCategoriesQuery(categoryFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        [FileValidatorFilter<UpdateCategoryCommand>([".png", ".jpg"], 1920 * 1080)]
        [HasPermission(Permission.UpdateCategory)]
        public async Task<ActionResult> UpadateCategory(Guid id, [FromForm] UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdateCategoryCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteCategory)]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpPost]
        [FileValidatorFilter<CreateCategoryCommand>([".png", ".jpg"], 1920 * 1080)]
        [HasPermission(Permission.CreateCategory)]
        public async Task<IActionResult> AddCategory([FromForm] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
