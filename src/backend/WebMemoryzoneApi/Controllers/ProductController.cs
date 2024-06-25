using Application.DTOs.Filters.Product;
using Application.Features.Products.Commands.AddProductImage;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.DeleteProductImage;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts([FromQuery] ProductFilter productFilter)
        {
            var result = await _mediator.Send(new GetListProductQuery(productFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpadateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetCategoryByUrlSlug(string slug)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
        [HttpPost("addimage")]
        public async Task<IActionResult> AddProductImage([FromForm] AddProductImageCommand command)
        {

            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
        [HttpDelete("{productId:Guid}/{imageId:Guid}")]
        public async Task<IActionResult> DeleteProductImage(Guid productId, Guid imageId)
        {
            var result = await _mediator.Send(new DeleteProductImageCommand(productId, imageId));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
    }
}
