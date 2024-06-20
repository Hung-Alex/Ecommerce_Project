using Application.DTOs.Filters.Categories;
using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.Get;
using Application.Features.Category.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;

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
        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(Id));
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories([FromQuery] CategoryFilter categoryFilter)
        {
            var result = await _mediator.Send(new GetListCategoriesQuery(categoryFilter));
            return Ok(result);
        }
        [HttpPut("{Id:Guid}")]
        [FileValidatorFilter<UpdateCategoryCommand>([".png", ".jpg"], 1 * 1024)]
        public async Task<ActionResult> UpadateBrand(Guid Id, [FromForm] UpdateCategoryCommand command)
        {
            if (Id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{Id:Guid}")]
        public async Task<ActionResult> DeleteCategory(Guid Id)
        {
            await _mediator.Send(new DeleteCategoryCommand(Id));
            return Ok();
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetCategoryByUrlSlug(string slug)
        {
            return Ok();
        }
        [HttpPost]
        [FileValidatorFilter<CreateCategoryCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<IActionResult> AddCategory([FromForm] CreateCategoryCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
