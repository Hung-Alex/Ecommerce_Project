using Application.Common.Interface;
using Domain.Shared;
using MediatR;
using Application.DTOs.Responses.Slides;
using Domain.Entities.Slides;
using Application.Features.Slides.Specification;
using Application.DTOs.Responses.Slides.SlideImages;


namespace Application.Features.Slides.Queries.Get
{
    public class GetListSlideQueriesHandler : IRequestHandler<GetListSlideQuery, Result<IEnumerable<SlideDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListSlideQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<SlideDTO>>> Handle(GetListSlideQuery request, CancellationToken cancellationToken)
        {
            var slideRepo = _unitOfWork.GetRepository<Slide>();
            var getSlideSpecification = new GetSlidesSpecification(request.SlideFilter);
            var slides = await slideRepo.GetAllAsync(getSlideSpecification);
            var totalItems = await slideRepo.CountAsync(getSlideSpecification);
            return new PagingResult<IEnumerable<SlideDTO>>(slides.Select(x => new SlideDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Images = x.SlidesImages
                    .Select(x => new SlideImageDTO()
                    {
                        Id = x.Id
                    })
            })
                , request.SlideFilter.PageNumber
                , request.SlideFilter.PageSize
                , totalItems);
        }
    }
}
