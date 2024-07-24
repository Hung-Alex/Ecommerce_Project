using Application.DTOs.Filters.Product;
using Application.Features.Products.Commands.AddProductImage;
using Application.Features.Products.Commands.AddProductVariant;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.DeleteProductVariants;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Commands.UpdateProductVariants;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetByUrlSlug;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPut("{productId:Guid}/{variantId:Guid}")]
        public async Task<ActionResult> UpadateProductVariant(Guid productId, [FromBody] UpdateProductVariantsCommand command)
        {
            if (productId != command.ProductId)
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
        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetCategoryByUrlSlug(string slug)
        {
            var result = await _mediator.Send(new GetProductByUrlSlugQuery(slug));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        [HttpDelete("{productId:Guid}/{variantId:Guid}")]
        public async Task<IActionResult> DeleteVariant(Guid productId, Guid variantId)
        {
            var result = await _mediator.Send(new DeleteProductVariantsCommand(productId, variantId));
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("addimage")]
        public async Task<IActionResult> AddProductImage([FromForm] AddProductImageCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
        [HttpPost("add-variant")]
        public async Task<ActionResult> AddProductVariant([FromBody] AddProductVariantCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
    }
}
