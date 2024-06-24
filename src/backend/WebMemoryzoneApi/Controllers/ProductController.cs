using Application.DTOs.Filters.Product;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(Id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts([FromQuery] ProductFilter productFilter)
        {
            var result = await _mediator.Send(new GetListProductQuery(productFilter));
            return Ok(result);
        }
        [HttpPut("{Id:Guid}")]
        public async Task<ActionResult> UpadateProduct(Guid Id, [FromForm] UpdateProductCommand command)
        {
            if (Id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{Id:Guid}")]
        public async Task<ActionResult> DeleteProduct(Guid Id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(Id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetCategoryByUrlSlug(string slug)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
    }
}
