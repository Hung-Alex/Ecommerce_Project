using Application.DTOs.Filters.Banner;
using Application.Features.Banners.Commands.CreateBanner;
using Application.Features.Banners.Commands.DeleteBanner;
using Application.Features.Banners.Commands.UpdateBanner;
using Application.Features.Banners.Queries.Get;
using Application.Features.Banners.Queries.GetBannerIsVisiable;
using Application.Features.Banners.Queries.GetById;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMemoryzoneApi.Filters;

namespace WebMemoryzoneApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/banners")]
    public class BannerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BannerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:Guid}")]
        [HasPermission(Domain.Enums.PermissionEnum.Permission.CreateBanner)]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBannerByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }      
        [HttpGet]
        public async Task<ActionResult> GetBanners([FromQuery] BannerFilter bannerFilter)
        {
            var result = await _mediator.Send(new GetListBannerQuery(bannerFilter));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("isvisable")]
        public async Task<IActionResult> GetBannerIsVisiable()
        {
            var result = await _mediator.Send(new GetBannerIsVisiableQuery());
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
            return Ok(result);
        }
        [HttpPost]
        [FileValidatorFilter<CreateBannerCommand>([".png", ".jpg"], 1024 * 1024)]
        public async Task<IActionResult> AddBanner([FromForm] CreateBannerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
