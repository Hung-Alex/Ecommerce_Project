using Application.Common.Interface;
using Application.CQRS.Brands.Commands.CreateBrand;
using Domain.Entities.Brands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebMemoryzoneApi.Filters;
using WebMemoryzoneApi.Model.Brand;
using WebMemoryzoneApi.Shared;

namespace WebMemoryzoneApi.Controllers
{
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
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> GetBrands([FromQuery] PagingModel pagingModel, [FromQuery] BrandFilters brandFilters)
        {

            return Ok();
        }
        [HttpPut("{Id:Guid}")]
        public async Task<ActionResult> UpadateBrand(Guid Id)
        {
            return Ok();
        }
        [HttpDelete("{Id:Guid}")]
        public async Task<ActionResult> DeleteBrand(Guid Id)
        {
            return Ok();
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult> GetBrandByUrlSlug(string slug)
        {
            return Ok();
        }
        [HttpPost]
        [FileValidatorFilter<CreateBrandCommand>([".png",".jpg"], 1024 * 1024)]
        public async Task<IActionResult> AddBrand([FromForm]CreateBrandCommand createBrandCommand)
        {
            await _mediator.Send(createBrandCommand);
            return Ok();
        }
    }
}
