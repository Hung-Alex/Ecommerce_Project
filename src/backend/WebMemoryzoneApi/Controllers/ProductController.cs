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
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadProduct, Permission.UpdateProduct])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (result.IsSuccess is false) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        [HasPermission(Permission.ReadProduct)]
        public async Task<ActionResult> GetProducts([FromQuery] ProductFilter productFilter)
        {
            var result = await _mediator.Send(new GetListProductQuery(productFilter));
            return Ok(result);
        }
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
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteProduct)]
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
