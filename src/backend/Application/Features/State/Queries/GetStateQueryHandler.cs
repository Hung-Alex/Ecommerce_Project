using Application.Common.Interface;
using Application.DTOs.Responses.State;
using Application.Features.State.Specification;
using AutoMapper;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Features.State.Queries
{
    public sealed class GetStateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetStateQuery, Result<IEnumerable<StateDTO>>>
    {
        public async Task<Result<IEnumerable<StateDTO>>> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Status>();
            var specification = new GetStateSpecification(request.filter);
            var states = await repo.GetAllAsync(specification);
            var totalItems = await repo.CountAsync(specification);
            return new PagingResult<IEnumerable<StateDTO>>(mapper.Map<IEnumerable<StateDTO>>(states), request.filter.PageNumber, request.filter.PageSize, totalItems);
        }
    }
}
