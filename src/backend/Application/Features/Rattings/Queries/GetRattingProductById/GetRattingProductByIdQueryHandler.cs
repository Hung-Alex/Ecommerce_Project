using Application.Common.Interface;
using Application.DTOs.Responses.Rattings;
using Application.Features.Rattings.Specification;
using AutoMapper;
using Domain.Entities.Rattings;
using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Queries.GetRattingProductById
{
    public class GetRattingProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetRattingProductByIdQuery, Result<IEnumerable<RattingDTO>>>
    {
        public async Task<Result<IEnumerable<RattingDTO>>> Handle(GetRattingProductByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Ratting>();
            var specification = new GetRattingProductByIdSpecification(request.Filter.ProductId);
            var rattings =await repo.GetAllAsync(specification);
            var totalItems = await repo.CountAsync(specification);
            return new PagingResult<IEnumerable<RattingDTO>>(mapper.Map<IEnumerable<RattingDTO>>(rattings), request.Filter.PageNumber, request.Filter.PageSize, totalItems);
        }
    }
}
