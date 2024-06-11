using Application.Common.Interface;
using Application.CQRS.Brands.Specification;
using Domain.Entities.Brands;
using Domain.Shared;
using MediatR;


namespace Application.CQRS.Brands.Queries
{
    public class GetListBrandQueriesHandler : IRequestHandler<GetListBrandQueries, Result<IEnumerable<Brand>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListBrandQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<Brand>>> Handle(GetListBrandQueries request, CancellationToken cancellationToken)
        {
            var brandRepo=_unitOfWork.GetRepository<Brand>();
            var result = await brandRepo.GetAllAsync(new GetBrandsSpecification(request.PagingParams.PageNumber, request.PagingParams.PageSize, request.Name));

            return new PagingResult<IEnumerable<Brand>>(null, 1,1,1);
        }
    }
}
