using Application.Common.Interface;
using AutoMapper;
using Domain.Shared;
using MediatR;
using Application.DTOs.Responses.Category;
using Domain.Entities.Category;
using Application.Features.Category.Specification;


namespace Application.Features.Category.Queries.Get
{
    public class GetListCategoryQueriesHandler : IRequestHandler<GetListCategoriesQuery, Result<IEnumerable<CategoryDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetListCategoryQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<CategoryDTO>>> Handle(GetListCategoriesQuery request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var getCategorySpecification = new GetCategoriesSpecification(request.CategoryFilter);
            var categories = await repoCategory.GetAllAsync(getCategorySpecification);
            var totalItems = await repoCategory.CountAsync(getCategorySpecification);
            return new PagingResult<IEnumerable<CategoryDTO>>(_mapper.Map<IEnumerable<CategoryDTO>>(categories), request.CategoryFilter.PageNumber, request.CategoryFilter.PageSize, totalItems);
        }
    }
}

