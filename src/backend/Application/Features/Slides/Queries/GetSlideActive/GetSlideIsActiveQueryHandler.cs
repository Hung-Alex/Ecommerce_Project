using Application.Common.Interface;
using Application.DTOs.Responses.Slides;
using Application.Features.Slides.Specification;
using AutoMapper;
using Domain.Entities.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Queries.GetSlideActive
{
    public sealed class GetSlideIsActiveQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetSlideIsActiveQuery, Result<IEnumerable<SlideDTO>>>
    {
        public async Task<Result<IEnumerable<SlideDTO>>> Handle(GetSlideIsActiveQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Slide>();
            var specification = new GetSlideIsActiveSpecification();
            var slides = await repo.GetAllAsync(specification);
            return Result<IEnumerable<SlideDTO>>.ResultSuccess(mapper.Map<IEnumerable<SlideDTO>>(slides));
        }
    }
}
