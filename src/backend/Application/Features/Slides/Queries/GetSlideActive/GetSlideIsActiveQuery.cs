using Application.DTOs.Responses.Slides;
using Domain.Shared;
using MediatR;
namespace Application.Features.Slides.Queries.GetSlideActive
{
    public record GetSlideIsActiveQuery:IRequest<Result<IEnumerable<SlideDTO>>>;
}
