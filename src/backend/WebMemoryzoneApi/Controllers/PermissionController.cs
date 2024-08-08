using Application.Features.Permissions.Queries.GetPermissions;
using Infrastructure.Services.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Domain.Enums.PermissionEnum;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a list of permissions
        /// </summary>
        /// <returns>A list of permissions</returns>
        [HttpGet]
        [HasPermission(Permission.ReadPermission)]
        public async Task<IActionResult> GetPermissions()
        {
            var result = await _mediator.Send(new GetPermissionsQuery());
            return Ok(result);
        }
    }
}
