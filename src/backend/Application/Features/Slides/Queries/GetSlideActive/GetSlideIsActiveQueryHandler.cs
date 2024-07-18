using Application.Common.Interface;
using Application.DTOs.Responses.Slides;
using Application.Features.Slides.Specification;
using Domain.Entities.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Queries.GetSlideActive
{
    public sealed class GetSlideIsActiveQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSlideIsActiveQuery, Result<SlideDTO>>
    {
        public async Task<Result<SlideDTO>> Handle(GetSlideIsActiveQuery request, CancellationToken cancellationToken)
        {

            var repo = unitOfWork.GetRepository<Slide>();
            var specification = new GetSlideIsActiveSpecification();
            var slide = await repo.FindOneAsync(specification);
            return Result<SlideDTO>.ResultSuccess(
                new SlideDTO
                {
                    Id = slide.Id
                    ,
                    Title = slide.Title
                    ,
                    Description = slide.Description
                    ,
                    Images = slide.SlidesImages.Select(x => x.ImageUrl).ToList()
                    ,
                    IsActive= slide.IsActive
                });
        }
    }
}
