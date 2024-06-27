using Application.DTOs.Filters.Slides;
using Application.DTOs.Responses.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Queries.Get
{
    public record GetListSlideQuery(SlideFilter SlideFilter) : IRequest<Result<IEnumerable<SlideDTO>>>;
}
