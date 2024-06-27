using Application.DTOs.Responses.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Queries.GetById
{
    public record GetSlideByIdQuery(Guid Id) : IRequest<Result<SlideDTO>>;
}
