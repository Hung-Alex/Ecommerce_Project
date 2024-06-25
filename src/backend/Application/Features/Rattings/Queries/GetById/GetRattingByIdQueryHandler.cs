using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.DTOs.Responses.Rattings;
using Domain.Entities.Rattings;
using Application.Features.Rattings.Specification;

namespace Application.Features.Rattings.Queries.GetById
{
    public sealed class GetRattingByIdQueryHandler : IRequestHandler<GetRattingByIdQuery, Result<RattingDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetRattingByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<RattingDTO>> Handle(GetRattingByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Ratting>();
            var getRattingByIdSpecification = new GetRattingByIdSepecification(request.Id);
            var ratting = await repo.FindOneAsync(getRattingByIdSpecification);
            if (ratting == null) return Result<RattingDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var rattingDTO = _mapper.Map<RattingDTO>(ratting);
            return Result<RattingDTO>.ResultSuccess(rattingDTO);
        }
    }
}
