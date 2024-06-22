using Application.DTOs.Filters.Categories;
using Application.DTOs.Filters.Product;
using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;

namespace WebMemoryzoneApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[HttpGet("{Id:Guid}")]
        //public async Task<ActionResult> GetById(Guid Id)
        //{
        //    var result = await _mediator.Send(new GetCategoryByIdQuery(Id));
        //    if (result == null) return NotFound();
        //    return Ok(result);
        //}
        [HttpGet]
        public async Task<ActionResult> GetCategories([FromQuery] ProductFilter productFilter)
        {
            var result = await _mediator.Send(new GetListProductQuery(productFilter));
            return Ok(result);
        }
        //[HttpPut("{Id:Guid}")]
        //[FileValidatorFilter<UpdateCategoryCommand>([".png", ".jpg"], 1 * 1024)]
        //public async Task<ActionResult> UpadateBrand(Guid Id, [FromForm] UpdateCategoryCommand command)
        //{
        //    if (Id != command.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}
        //[HttpDelete("{Id:Guid}")]
        //public async Task<ActionResult> DeleteCategory(Guid Id)
        //{
        //    await _mediator.Send(new DeleteCategoryCommand(Id));
        //    return Ok();
        //}
        //[HttpGet("{slug}")]
        //public async Task<ActionResult> GetCategoryByUrlSlug(string slug)
        //{
        //    return Ok();
        //}
        [HttpPost]
        [FileValidatorFilter<CreateProductCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<IActionResult> AddCategory([FromForm] CreateProductCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
