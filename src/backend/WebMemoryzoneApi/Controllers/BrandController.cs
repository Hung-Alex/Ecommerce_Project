using Application.DTOs.Filters.Brands;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.Get;
using Application.Features.Brands.Queries.GetById;
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
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBrandByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        // [HasPermission(PermissionOperator.Or, [Permission.CreateBanner, Permission.CreateBrand, Permission.CreateProduct])]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetBrands([FromQuery] BrandFilter brandFilter)
        {
            var result = await _mediator.Send(new GetListBrandsQuery(brandFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        [FileValidatorFilter<UpdateBrandCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<ActionResult> UpadateBrand(Guid id, [FromForm] UpdateBrandCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetBrandByUrlSlug(string slug)
        {
            return Ok();
        }
        [HttpPost]
        [FileValidatorFilter<CreateBrandCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<IActionResult> AddBrand([FromForm] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
    }
}
