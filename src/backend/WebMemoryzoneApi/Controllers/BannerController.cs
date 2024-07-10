using Application.DTOs.Filters.Banner;
using Application.Features.Banners.Commands.CreateBanner;
using Application.Features.Banners.Commands.DeleteBanner;
using Application.Features.Banners.Commands.UpdateBanner;
using Application.Features.Banners.Queries.Get;
using Application.Features.Banners.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;

namespace WebMemoryzoneApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("apis/banners")]
    public class BannerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BannerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBannerByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetBanners([FromQuery] BannerFilter bannerFilter)
        {
            var result = await _mediator.Send(new GetListBannerQuery(bannerFilter));
            return Ok(result);
        }
        [HttpPut("{id:Guid}")]
        [FileValidatorFilter<UpdateBannerCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<ActionResult> UpadateBanner(Guid id, [FromForm] UpdateBannerCommand command)
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
        public async Task<ActionResult> DeleteBanner(Guid id)
        {
            var result = await _mediator.Send(new DeleteBannerCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok();
        }
        [HttpPost]
        [FileValidatorFilter<CreateBannerCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<IActionResult> AddBanner([FromForm] CreateBannerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok();
        }
    }
}
