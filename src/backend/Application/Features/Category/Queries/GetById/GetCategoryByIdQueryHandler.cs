using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Application.DTOs.Responses.Category;
using Domain.Entities.Category;
using Application.Features.Category.Specification;

namespace Application.Features.Category.Queries.GetById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Categories>();
            var getCategoryByIdSpecification = new GetCategoryByIdSepecification(request.Id);
            var category = await repo.FindOneAsync(getCategoryByIdSpecification);
            if (category == null) return null;
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
