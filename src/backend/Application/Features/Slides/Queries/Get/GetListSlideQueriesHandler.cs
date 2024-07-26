using Application.Common.Interface;
using Domain.Shared;
using MediatR;
using Application.DTOs.Responses.Slides;
using Domain.Entities.Slides;
using Application.Features.Slides.Specification;
using AutoMapper;


namespace Application.Features.Slides.Queries.Get
{
    public class GetListSlideQueriesHandler : IRequestHandler<GetListSlideQuery, Result<IEnumerable<SlideDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetListSlideQueriesHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<SlideDTO>>> Handle(GetListSlideQuery request, CancellationToken cancellationToken)
        {
            var slideRepo = _unitOfWork.GetRepository<Slide>();
            var getSlideSpecification = new GetSlidesSpecification(request.SlideFilter);
            var slides = await slideRepo.GetAllAsync(getSlideSpecification);
            var totalItems = await slideRepo.CountAsync(getSlideSpecification);
            return new PagingResult<IEnumerable<SlideDTO>>(_mapper.Map<IEnumerable<SlideDTO>>(slides),request.SlideFilter.PageNumber,request.SlideFilter.PageSize,totalItems);
        }
    }
}
