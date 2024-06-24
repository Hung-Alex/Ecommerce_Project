using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.Get;
using Application.Features.Brands.Queries.GetById;
using Application.DTOs.Filters.Brand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;

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
        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var result = await _mediator.Send(new GetBrandByIdQuery(Id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetBrands([FromQuery] BrandFilter productFilter)
        {
            var result = await _mediator.Send(new GetListBrandQuery(productFilter));
            return Ok(result);
        }
        [HttpPut("{Id:Guid}")]
        [FileValidatorFilter<UpdateBrandCommand>([".png", ".jpg"], 1 * 1024)]
        public async Task<ActionResult> UpadateBrand(Guid Id, [FromForm] UpdateBrandCommand command)
        {
            if (Id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("{Id:Guid}")]
        public async Task<ActionResult> DeleteBrand(Guid Id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(Id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
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
