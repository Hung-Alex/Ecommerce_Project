using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Application.DTOs.Responses.Category;
using Domain.Entities.Category;
using Application.Features.Category.Specification;
using Domain.Shared;
using Domain.Constants;

namespace Application.Features.Category.Queries.GetById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<CategoryDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Categories>();
            var getCategoryByIdSpecification = new GetCategoryByIdSepecification(request.Id);
            var category = await repo.FindOneAsync(getCategoryByIdSpecification);
            if (category == null) return Result<CategoryDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Result<CategoryDTO>.ResultSuccess(categoryDTO);
        }
    }
}
