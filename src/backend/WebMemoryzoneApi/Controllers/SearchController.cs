﻿using Application.DTOs.Filters.Search;
using Application.Features.Search.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/searchs")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Searches for items based on the provided filter
        /// </summary>
        /// <param name="searchFilter">The search filter</param>
        /// <returns>A list of search results if successful, otherwise a 400 result</returns>
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchFilter searchFilter)
        {
            var result = await _mediator.Send(new SearchQuery(searchFilter));
            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
