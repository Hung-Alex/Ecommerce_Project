using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.DTOs.Responses.Slides;
using Domain.Entities.Slides;
using Application.Features.Slides.Specification;

namespace Application.Features.Slides.Queries.GetById
{
    public sealed class GetSlideByIdQueryHandler : IRequestHandler<GetSlideByIdQuery, Result<SlideDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetSlideByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<SlideDTO>> Handle(GetSlideByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Slide>();
            var getSlideByIdSpecification = new GetSlideDetailAndImageSpecification(request.Id);
            var slide = await repo.FindOneAsync(getSlideByIdSpecification);
            if (slide == null) return Result<SlideDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var productDTO = new SlideDTO()
            {
                Id = slide.Id,
                Title = slide.Title,
                Description = slide.Description,
                Images = slide.SlidesImages.Select(x => x.ImageUrl)
            };
            return Result<SlideDTO>.ResultSuccess(productDTO);
        }
    }
}
