using Application.DTOs.Filters.Product;
using Application.Features.Products.Commands.AddProductImage;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetByUrlSlug;
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
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a product by its ID
        /// </summary>
        /// <param name="id">The ID of the product</param>
        /// <returns>The product if found, otherwise a 404 result</returns>
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadProduct, Permission.UpdateProduct])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Gets a list of products
        /// </summary>
        /// <param name="productFilter">The filter to apply to the products</param>
        /// <returns>A list of products</returns>
        [HttpGet]
        [HasPermission(Permission.ReadProduct)]
        public async Task<ActionResult> GetProducts([FromQuery] ProductFilter productFilter)
        {
            var result = await _mediator.Send(new GetListProductQuery(productFilter));
            return Ok(result);
        }
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id">The ID of the product to update</param>
        /// <param name="command">The update command</param>
        /// <returns>The updated product if successful, otherwise a 400 result</returns>
        [HttpPut("{id:Guid}")]
        [HasPermission(Permission.UpdateProduct)]
        public async Task<ActionResult> UpadateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdateProductCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess is false) return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">The ID of the product to delete</param>
        /// <returns>A 200 result if successful, otherwise a 404 result</returns>
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteProduct)]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Gets a product by its URL slug
        /// </summary>
        /// <param name="slug">The URL slug of the product</param>
        /// <returns>The product if found, otherwise a 404 result</returns>
        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetCategoryByUrlSlug(string slug)
        {
            var result = await _mediator.Send(new GetProductByUrlSlugQuery(slug));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="command">The create command</param>
        /// <returns>The created product if successful, otherwise a 400 result</returns>
        [HttpPost]
        [FileValidatorFilter<CreateProductCommand>([".png", ".jpg"], 1920 * 1080)]
        [HasPermission(Permission.CreateProduct)]
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
        /// <summary>
        /// Adds an image to a product
        /// </summary>
        /// <param name="command">The add image command</param>
        /// <returns>The result of the operation</returns>
        [HttpPost("add-image")]
        [FileValidatorFilter<AddProductImageCommand>([".png", ".jpg"], 1920 * 1080)]
        [HasPermission(Permission.UploadProductImage)]
        public async Task<IActionResult> AddProductImage([FromForm] AddProductImageCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

    }
}
