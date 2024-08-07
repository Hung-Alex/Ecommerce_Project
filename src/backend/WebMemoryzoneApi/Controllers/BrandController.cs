using Application.DTOs.Filters.Brands;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.Get;
using Application.Features.Brands.Queries.GetById;
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
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        [HasPermission(PermissionOperator.Or, [Permission.ReadBrand, Permission.UpdateBrand])]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBrandByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetBrands([FromQuery] BrandFilter brandFilter)
        {
            var result = await _mediator.Send(new GetListBrandsQuery(brandFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        [FileValidatorFilter<UpdateBrandCommand>([".png", ".jpg"], 1024 * 1024)]
        [HasPermission(Permission.UpdateBrand)]
        public async Task<ActionResult> UpadateBrand(Guid id, [FromForm] UpdateBrandCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(Result<UpdateBrandCommand>.ResultFailures(ErrorConstants.InvalidId));
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{id:Guid}")]
        [HasPermission(Permission.DeleteBrand)]
        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpPost]
        [FileValidatorFilter<CreateBrandCommand>([".png", ".jpg"], 1024 * 1024)]
        [HasPermission(Permission.CreateBrand)]
        public async Task<IActionResult> AddBrand([FromForm] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
