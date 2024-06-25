using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Domain.Shared;
using Domain.Constants;
using Application.DTOs.Responses.Tags;
using Application.Features.Tags.Specification;
using Domain.Entities.Tags;
using Application.DTOs.Responses.Rattings;

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
            var repo = _unitOfWork.GetRepository<Tag>();
            var getTagByIdSpecification = new GetTagByIdSepecification(request.Id);
            var tag = await repo.FindOneAsync(getTagByIdSpecification);
            if (tag == null) return Result<TagDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var categoryDTO = _mapper.Map<TagDTO>(tag);
            return Result<TagDTO>.ResultSuccess(categoryDTO);
        }
    }
}
